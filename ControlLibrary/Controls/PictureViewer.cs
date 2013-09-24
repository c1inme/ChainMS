using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using WeakEvents;

namespace ControlLibrary
{
    public class PictureViewer : Control, IDisposable, INotifyPropertyChanged
    {
        private const int PIN_WIDTH = 128;
        private const int PIN_HEIGHT = 128;
        public event EventHandler IsElementSelectedChanged;
        public event EventHandler IsElementSelectedAtextChanged;
        public event EventHandler CurrentScaleChanged;
        public event EventHandler NotifyDrawBrushWidthChanged;
        public event EventHandler LoadImageCompleted;
        public event EventHandler CurrentNoteChanged;
        public event EventHandler<OrigineClickedEventArgs> OrigineClicked;
        public event EventHandler MouseIsIn;

        private bool _isMouseIn = false;
        private ItemPin currentItemPin;
        private bool _mouseDownActive;
        private Point _mouseDownInitialRenderCenter;
        private Point _mouseDownInitialLocation;
        private FrameworkElement _currentElement;
        private Point _currentPolyLinePoint;
        private BlinkerRectangle _currentNotifyRectangle;
        private bool _imageLoadedComplete;
        private int _notifyDrawBrushWidth = 12;
        private double _currentScale;

        private Rectangle _shapeMover;
        private FrameworkElement _previousSelectedElement;
        private readonly ObservableCollection<HightlightCriteria> _highlightList = new ObservableCollection<HightlightCriteria>();
        private string _tilesPath;
        private readonly List<Layer> _layers = new List<Layer>();
        private Point _renderCenter; //location of the center of the image in the viewPort
        private int _renderLevel = -1;
        private int _pictureWidth;
        private int _pictureHeight;
        private int _tileSize = 1;
        private readonly Ellipse _cursor = new Ellipse() { Width = 6, Height = 6, Visibility = Visibility.Collapsed };

        private int _rotateAngle = 0;
        private int _pageIndex = 0;
        private bool _showPin = false;
        private int _highlightIndex = 0;
        private bool _IsShowhighlightFirst = false;
        private double[] scales;
        private int rlPixelWidth;
        private int rlPixelHeight;

        private MouseWheelHelper _mouseWheelHelper;
        //private ObservableCollection<Note> notesList = new ObservableCollection<Note>();
        private readonly ObservableCollection<DocLink> _linksList = new ObservableCollection<DocLink>();
        private readonly ObservableCollection<FrameworkElement> _currentNotifyElementList = new ObservableCollection<FrameworkElement>();

        static int _controlCount;
        static readonly object synchroObject = new object();
        readonly int controlIndex;
        private ServiceClient _serviceClient;
        private Rectangle _rectSelectedArea;

        //Raise event selected an area
        public event EventHandler SelectedAreaHandler;
        public PictureViewer()
        {
            SetViewPortLocationIsEnabled = true;

            lock (synchroObject)
            {
                controlIndex = _controlCount;
                _controlCount++;
            }

            _mouseWheelHelper = new MouseWheelHelper(this);
            WeakEventHelper.RegisterMouseWheelMoved<PictureViewer>(_mouseWheelHelper, this, (ls, s, e) => ls.mouseWheelHelper_Moved(s, e));
            this.Loaded += new RoutedEventHandler(PictureViewer_Loaded);
            //notesList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(notificationsList_CollectionChanged);
            _linksList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(linksList_CollectionChanged);
            _currentNotifyElementList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(currentNotifyElementList_CollectionChanged);

            _serviceClient = new ServiceClient();

            SimpleWeakEventPattern.ObjectTracker.Track(this);
        }

        public void Dispose()
        {
            this.Loaded -= new RoutedEventHandler(PictureViewer_Loaded);
            //notesList.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(notificationsList_CollectionChanged);
            _linksList.CollectionChanged -= linksList_CollectionChanged;
            _currentNotifyElementList.CollectionChanged -= currentNotifyElementList_CollectionChanged;

            if (isTemplateApplied)
            {
                gridOptionsBtn.Checked -= gridOptionsBtn_Checked;
                gridOptionsBtn.Unchecked -= gridOptionsBtn_Unchecked;
            }

            HideCurrentShapeMover();
            HideCurrentShapeHandlers();
            DisposeTiles();
            DisposeNotes();
            CurrentNote = null;

            _serviceClient.Dispose();
            _serviceClient = null;

            _mouseWheelHelper = null;

            DisposeSelectedArea();

            if (higlightBoard != null)
            {
                higlightBoard.Stop();
                higlightBoard.Children.Clear();
                higlightBoard = null;
            }

            if (blinkCompareCanvasSB != null)
            {
                blinkCompareCanvasSB.Children.Clear();
                blinkCompareCanvasSB = null;
            }
        }


        public void DisposeSelectedArea()
        {
            _rectSelectedArea = null;
            IsSelectedArea = false;
            if (selectedAreaCanvas != null)
                selectedAreaCanvas.Children.Clear();

            Rectangles.Clear();
        }

        List<Rectangle> rectangles = new List<Rectangle>();
        public List<Rectangle> Rectangles
        {
            get { return rectangles; }
            set { rectangles = value; }
        }

        public Rectangle RectSelectedArea
        {
            get { return _rectSelectedArea; }
            set
            {
                _rectSelectedArea = value;

                if (_rectSelectedArea != null)
                {
                    selectedAreaCanvas.Children.Add(_rectSelectedArea);


                }
            }
        }

        public void ApplyRectanglesToSelectedAreaCanvas(List<Rectangle> rects)
        {
            foreach (Rectangle rect in rects)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = rect.Width;
                rectangle.Height = rect.Height;
                rectangle.Fill = rect.Fill;
                rectangle.Opacity = rect.Opacity;
                rectangle.Stroke = rect.Stroke;
                selectedAreaCanvas.Children.Add(rectangle);
                Canvas.SetLeft(rectangle, Canvas.GetLeft(rect));
                Canvas.SetTop(rectangle, Canvas.GetTop(rect));
                Rectangles.Add(rectangle);
            }

            UpdateSelectedArea();
        }

        private bool IsOverlapsRectangles(double y, double height)
        {
            foreach (Rectangle rect in rectangles)
            {
                double rectY = Canvas.GetTop(rect);
                if ((rectY <= y && rect.Height + rectY >= y)
                    ||
                    (y <= rectY && y + height >= rectY)
                    )
                    return true;
            }

            return false;
        }

        /*void SelectedArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!SelectedAred || rectSelectedArea != null) return;

            rectSelectedArea = new Rectangle();
            //origPointSelectedArea = e.GetPosition(highlightCanvas);
            origPointSelectedArea = e.GetPosition(highlightCanvas);
            Canvas.SetLeft(rectSelectedArea, origPointSelectedArea.X);
            Canvas.SetTop(rectSelectedArea, origPointSelectedArea.Y);
            rectSelectedArea.Stroke = new SolidColorBrush(Colors.Black);
            rectSelectedArea.StrokeThickness = 1;

            SolidColorBrush brush = new SolidColorBrush(Colors.Yellow);
            brush.Opacity = 0.3;
            rectSelectedArea.Fill = brush;

            this.highlightCanvas.Children.Add(rectSelectedArea);

            this.highlightCanvas.MouseMove += draw_MouseMove;
            this.highlightCanvas.MouseLeftButtonUp += draw_MouseLeftButtonUp;

            rectSelectedArea.Width = Canvas.GetLeft(this.colHeaders) - origPointSelectedArea.X + this.colHeaders.Width;
        }

        private void draw_MouseMove(object sender, MouseEventArgs e)
        {
            if (rectSelectedArea != null)
            {
                Point curPoint = e.GetPosition(this.highlightCanvas);
             

                if (curPoint.Y > origPointSelectedArea.Y)
                {
                    rectSelectedArea.Height = curPoint.Y - origPointSelectedArea.Y;
                }
                else if (curPoint.Y < origPointSelectedArea.Y)
                {
                    Canvas.SetTop(rectSelectedArea, curPoint.Y);
                    rectSelectedArea.Height = origPointSelectedArea.Y - curPoint.Y;
                }
            }
        }

        private void draw_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (rectSelectedArea != null)
            {
                this.highlightCanvas.MouseMove -= draw_MouseMove;
                this.highlightCanvas.MouseLeftButtonUp -= draw_MouseLeftButtonUp;                

                Layer currentLayer = layers[RenderLevel];

                float ratioX = currentLayer.PixelWidth/ this.pictureWidth;
                float ratioY = currentLayer.TileHeight / this.pictureHeight;
                
                var pageTop = Math.Floor(renderCenter.Y - (this.pictureHeight / 2.0));
                var pageLeft = Math.Floor(renderCenter.X - (this.pictureWidth / 2.0));

                double selectX = origPointSelectedArea.X - pageLeft;
                double selectY = origPointSelectedArea.X - pageTop;                
                
               
                //ServiceManager.Instance.GetSelectedArea(this.PlanID, selectX * ratioX, selectY * ratioY,
               //     rectSelectedArea.Width * ratioX, rectSelectedArea.Height * ratioY, this, Service_GetSelectedAreaCompleted);
            }
        }

        void Service_GetSelectedAreaCompleted(object sender, GetSelectedAreaCompletedEventArgs e)
        {
            SelectedAred = false;
        }*/

        //public void ApplyTextsDiff(CustomRectangleF rect)
        //{
        //    Rectangle rectangle = new Rectangle();
        //    rectangle.Width = rect.Width;
        //    rectangle.Height = rect.Height;
        //    rectangle.Fill = new SolidColorBrush(Colors.Green);
        //    rectangle.Opacity = 0.3;
        //    selectedAreaCanvas.Children.Add(rectangle);
        //    Canvas.SetLeft(rectangle, rect.X);
        //    Canvas.SetTop(rectangle, rect.Y);
        //    //Rectangles.Add(rectangle);                
        //}

        public void UpdateSelectedArea()
        {
            if (this.Visibility == System.Windows.Visibility.Collapsed)
                return;

            if (_layers.Count - 1 < RenderLevel)
                return;

            Layer l = _layers[RenderLevel];
            UpdateSelectedArea(GetCurrentScale(), l.PixelWidth, l.PixelHeight);

        }

        private void UpdateSelectedArea(double renderScale, int rlPixelWidth, int rlPixelHeight)
        {
            if (this.Visibility == Visibility.Collapsed)
                return;

            if (selectedAreaCanvas != null)
            {

                var imageTop = Math.Floor(_renderCenter.Y - (rlPixelHeight / 2.0));
                var imageLeft = Math.Floor(_renderCenter.X - (rlPixelWidth / 2.0));


                //System.Diagnostics.Debug.WriteLine("[" + Canvas.GetLeft(SelectedAreaCanvas) + " : "  + Canvas.GetTop(SelectedAreaCanvas) + "]");
                //System.Diagnostics.Debug.WriteLine("[" + xx + " : " + yy + "]");

                Transform fullRotateTransform = null;
                switch (_rotateAngle)
                {
                    case 90:
                        RotateTransform drawingCanvasRotate90Transform = new RotateTransform();
                        drawingCanvasRotate90Transform.CenterX = 0;
                        drawingCanvasRotate90Transform.CenterY = 0;
                        drawingCanvasRotate90Transform.Angle = 90;

                        TranslateTransform drawingCanvasTranslate90Transform = new TranslateTransform();
                        drawingCanvasTranslate90Transform.X = rlPixelWidth;
                        drawingCanvasTranslate90Transform.Y = 0;

                        fullRotateTransform = new TransformGroup();
                        ((TransformGroup)fullRotateTransform).Children.Add(drawingCanvasRotate90Transform);
                        ((TransformGroup)fullRotateTransform).Children.Add(drawingCanvasTranslate90Transform);
                        break;

                    case 180:
                        RotateTransform drawingCanvasRotate180Transform = new RotateTransform();
                        drawingCanvasRotate180Transform.CenterX = rlPixelWidth / 2;
                        drawingCanvasRotate180Transform.CenterY = rlPixelHeight / 2;
                        drawingCanvasRotate180Transform.Angle = 180;

                        fullRotateTransform = drawingCanvasRotate180Transform;
                        break;

                    case 270:
                        RotateTransform drawingCanvasRotate270Transform = new RotateTransform();
                        drawingCanvasRotate270Transform.CenterX = 0;
                        drawingCanvasRotate270Transform.CenterY = 0;
                        drawingCanvasRotate270Transform.Angle = 270;

                        TranslateTransform drawingCanvasTranslate270Transform = new TranslateTransform();
                        drawingCanvasTranslate270Transform.X = 0;
                        drawingCanvasTranslate270Transform.Y = rlPixelHeight;

                        fullRotateTransform = new TransformGroup();
                        ((TransformGroup)fullRotateTransform).Children.Add(drawingCanvasRotate270Transform);
                        ((TransformGroup)fullRotateTransform).Children.Add(drawingCanvasTranslate270Transform);
                        break;

                }

                ScaleTransform scaleTransform = null;
                double scale = renderScale;
                if (scale == 1.0)
                {
                    scaleTransform = null;
                }
                else
                {
                    scaleTransform = new ScaleTransform();
                    scaleTransform.ScaleX = scale;
                    scaleTransform.ScaleY = scale;
                    scaleTransform.CenterX = 0;
                    scaleTransform.CenterY = 0;

                }

                if (fullRotateTransform == null)
                {
                    selectedAreaCanvas.RenderTransform = scaleTransform;
                }
                else
                {
                    if (scaleTransform == null)
                    {
                        selectedAreaCanvas.RenderTransform = fullRotateTransform;
                    }
                    else
                    {
                        TransformGroup scaleAndRotateTransform = new TransformGroup();
                        scaleAndRotateTransform.Children.Add(scaleTransform);
                        scaleAndRotateTransform.Children.Add(fullRotateTransform);

                        selectedAreaCanvas.RenderTransform = scaleAndRotateTransform;
                    }
                }


                Canvas.SetLeft(selectedAreaCanvas, _renderCenter.X - rlPixelWidth / 2.0);
                Canvas.SetTop(selectedAreaCanvas, _renderCenter.Y - rlPixelHeight / 2.0);

            }
        }

        public void SetNoteTwin(PictureViewer pv)
        {
            //notesList.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(notificationsList_CollectionChanged);
            //notesList = pv.NoteList;
            //notesList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(notificationsList_CollectionChanged);
        }


        private bool isLoaded;
        void PictureViewer_Loaded(object sender, RoutedEventArgs e)
        {
            isLoaded = true;

            UpdateTiles(this.ActualWidth, this.ActualHeight);
        }

        public void Initialize()
        {
            UpdateTiles(this.ActualWidth, this.ActualHeight);
            InitializeGrid();
            UpdateGrid();

            UpdateDrawingCanvas();

            if (CurrentNotifyRectangle != null)
            {
                if (drawingCanvas != null)
                {
                    drawingCanvas.Children.Clear();
                    _shapeMover = null;
                    _shapeHandler1 = null;
                    _shapeHandler2 = null;
                    ClearCurrentNotifShapeList();
                    drawingCanvas.Children.Add(CurrentNotifyRectangle);
                }
            }
        }

        private void ClearCurrentNotifShapeList()
        {
            foreach (FrameworkElement shape in _currentNotifyElementList)
            {
                shape.MouseLeftButtonDown -= new MouseButtonEventHandler(currentShape_MouseLeftButtonDown);

                TextBox textBox = shape as TextBox;
                if (textBox != null)
                {
                    textBox.TextChanged -= new TextChangedEventHandler(textBox_TextChanged);
                }
            }

            _currentNotifyElementList.Clear();
        }

        private bool isAPlanShown = false;

        public void Show(int planId, int versionNbr, Guid folderGuid, int currentPageNbr, string tilesPath, int width, int height, GridOptions gridOptions, int? nbrOfZoomLevels, int tileSize, int bigThumbWidth, int bigThumbHeight, int rotateAngle)
        {
            Show(planId, versionNbr, folderGuid, currentPageNbr, tilesPath, width, height, gridOptions, nbrOfZoomLevels, tileSize, bigThumbWidth, bigThumbHeight, -1, new Point(-1, -1), false, false, rotateAngle);
        }


        public void Show(int planId, int versionNbr, Guid folderGuid, int currentPageNbr, string tilesPath, int width, int height, GridOptions gridOptions, int? nbrOfZoomLevels, int tileSize, int bigThumbWidth, int bigThumbHeight, int iniRenderLevel, Point iniRenderCenter, bool useIniRenderCenter, bool useIniRenderLevel, int rotateAngle, Point pinPosition = new Point(), bool showPin = false)
        {
            ErrorLoadingTiles = false;

            if (planId == this.PlanId)
            {
                if (versionNbr == this.VersionNbr)
                {
                    if (currentPageNbr == this._pageIndex)
                    {
                        if (!useIniRenderCenter || (iniRenderCenter == this.RenderCenter))
                        {
                            if (!useIniRenderLevel || (iniRenderLevel == this.RenderLevel))
                            {
                                if (rotateAngle == this._rotateAngle)
                                {
                                    if (showPin == this._showPin)
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            _imageLoadedComplete = false;
            //F12012: Visibility on plan - to improve UI
            if (IsPlanInvisible)
            {
                ShowMessageNoAccessPlan();
                return;
            }

            HiddenMessageNoAccessPlan();
            HiddenMessageSearchIndex(planId < 0);

            if (Global.Instance.CurrentUser != null && (planId > 0) && (planId != PlanId) && (this.Visibility == Visibility.Visible))
            {
                _serviceClient.SetDocumentIsAccessedAsync(planId);
            }
            //IList<Rect> hightlightSearched = null
            //Debug.WriteLine("Showing image " + pictureName + ", width = " + width + ", height = " + height);
            DisposeTiles();
            DisposeSelectedArea();

            //if (planID != this.PlanID)
            //    DisposeNotes();

            this.PlanId = planId;
            this.VersionNbr = versionNbr;
            this.FolderGuid = folderGuid;
            this._pageIndex = currentPageNbr;
            this._showPin = showPin;

            int currentBigThumbWidth = bigThumbWidth;
            int currentBigThumbHeight = bigThumbHeight;
            switch (rotateAngle)
            {
                case 0:
                case 180:
                    _pictureWidth = width;
                    _pictureHeight = height;
                    currentBigThumbWidth = bigThumbWidth;
                    currentBigThumbHeight = bigThumbHeight;
                    break;

                case 90:
                case 270:
                    _pictureWidth = height;
                    _pictureHeight = width;
                    currentBigThumbWidth = bigThumbHeight;
                    currentBigThumbHeight = bigThumbWidth;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("Invalid rotate angle value");
            }


            this._tileSize = tileSize;
            this._rotateAngle = rotateAngle;

            this.gridOptions = gridOptions;


            this.gridOptions.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(gridOptions_PropertyChanged);

            if ((nbrOfZoomLevels == null)) // || (nbrOfZoomLevels == 0))
            {
                DisplayStatus = PVDisplayStatus.Broken;
                return;
            }

            isAPlanShown = true;
            Cursor = Cursors.Hand;

            _tilesPath = tilesPath.Trim('/');

            DisplayStatus = PVDisplayStatus.Ok;

            int currentWidth = _pictureWidth;
            int currentHeight = _pictureHeight;
            int choosenRenderLevel = -1;
            int maxLevel = -1;
            for (int level = 0; level < nbrOfZoomLevels; level++)
            {
                maxLevel++;

                if ((currentWidth > this.ActualWidth) || (currentHeight > this.ActualHeight))
                    choosenRenderLevel = maxLevel;


                _layers.Add(new Layer(maxLevel, currentWidth, currentHeight, tileSize, false));
                currentWidth = (int)Math.Ceiling((double)currentWidth / 2.0);
                currentHeight = (int)Math.Ceiling((double)currentHeight / 2.0);

            }

            maxLevel++;
            _layers.Add(new Layer(maxLevel, currentBigThumbWidth, currentBigThumbHeight, tileSize, true));
            if ((currentBigThumbWidth > this.ActualWidth) || (currentBigThumbHeight > this.ActualHeight))
                choosenRenderLevel = maxLevel;

            choosenRenderLevel++;
            if (choosenRenderLevel > maxLevel)
                choosenRenderLevel = maxLevel;

            if (useIniRenderLevel)
            {
                if (iniRenderLevel > maxLevel)
                    RenderLevel = maxLevel;
                else
                    RenderLevel = iniRenderLevel;
            }
            else
            {
                RenderLevel = choosenRenderLevel;
            }

            if (useIniRenderCenter)
            {
                _renderCenter = iniRenderCenter;
            }
            else
            {
                _renderCenter = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
                int rlPixelWidth = _layers[RenderLevel].PixelWidth;
                int rlPixelHeight = _layers[RenderLevel].PixelHeight;

                if ((_renderCenter.Y - rlPixelHeight / 2) < 0)
                {
                    _renderCenter.Y = rlPixelHeight / 2;
                }

                if ((_renderCenter.X - rlPixelWidth / 2) < 0)
                {
                    _renderCenter.X = rlPixelWidth / 2;
                }
            }



            // locate Pin necessary
            LocatePin(pinPosition, showPin, !useIniRenderCenter);

            UpdateTiles(this.ActualWidth, this.ActualHeight);
            InitializeGrid();
            UpdateGrid();

            UpdateDrawingCanvas();

            RefreshCurrentNoteDisplay();
            RefreshTransformPin();

        }

        private void UpdateBorderCanvas()
        {
            if (borderCanvas == null)
                return;

            borderCanvas.Children.Clear();

            Rectangle planBorder = new Rectangle();
            planBorder.Stroke = new SolidColorBrush(Colors.Gray);
            planBorder.StrokeThickness = 10;

            if (_rotateAngle == 0 || _rotateAngle == 180 || _rotateAngle == -180 || _rotateAngle == 360)
            {
                planBorder.Width = _pictureWidth;
                planBorder.Height = _pictureHeight;
            }
            else
            {
                planBorder.Width = _pictureHeight;
                planBorder.Height = _pictureWidth;
            }
            borderCanvas.Children.Add(planBorder);
        }

        public void UpdateHighlights(IList<Rect> highlights)
        {
            // convert highlight criteria corresponding by RenderLevel
            highlightCanvas.Children.Clear();
            _highlightList.Clear();
            higlightBoard.Stop();
            higlightBoard.Children.Clear();
            foreach (var item in highlights)
            {
                var orgRect = new Rect(item.X, item.Y, item.Width, item.Height);
                _highlightList.Add(new HightlightCriteria() { OriginalRect = orgRect });
            }
            _IsShowhighlightFirst = true;
            UpdateTiles(this.ActualWidth, this.ActualHeight);
        }


        public Point RenderCenter
        {
            get { return _renderCenter; }
            set
            {
                if (_renderCenter != value)
                {
                    _renderCenter = value;
                    UpdateTiles(this.ActualWidth, this.ActualHeight);
                }
            }
        }

        public int RenderLevel
        {
            get { return _renderLevel; }
            set
            {
                if (_renderLevel != value)
                {
                    _renderLevel = value;
                    if (isAPlanShown)
                        UpdateTiles(this.ActualWidth, this.ActualHeight);
                    OnRenderLevelChanged();
                }
            }
        }

        public event EventHandler RenderLevelChanged;
        protected void OnRenderLevelChanged()
        {
            if (RenderLevelChanged != null)
                RenderLevelChanged(this, EventArgs.Empty);
        }

        private bool isUserInteractionEnabled = true;
        public bool IsUserInteractionEnabled
        {
            get { return isUserInteractionEnabled; }
            set
            {
                if (isUserInteractionEnabled != value)
                {
                    isUserInteractionEnabled = value;
                }
            }
        }


        void notificationsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (Note notification in e.OldItems)
                {
                    BlinkerRectangle notifyRectangleToRemove = null;

                    if (drawingCanvas != null)
                    {
                        foreach (UIElement element in drawingCanvas.Children)
                        {
                            BlinkerRectangle blinkerRectangle = element as BlinkerRectangle;
                            if ((blinkerRectangle != null) && (blinkerRectangle.GuidID == notification.GuidId))
                            {
                                notifyRectangleToRemove = blinkerRectangle;
                                break;
                            }
                        }

                        if (notifyRectangleToRemove != null)
                        {
                            drawingCanvas.Children.Remove(notifyRectangleToRemove);
                        }
                    }
                }
            }

        }

        void linksList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (DocLink link in e.OldItems)
                {
                    //BlinkerRectangle notifyRectangleToRemove = null;

                    //if (drawingCanvas != null)
                    //{
                    //    foreach (UIElement element in drawingCanvas.Children)
                    //    {
                    //        BlinkerRectangle blinkerRectangle = element as BlinkerRectangle;
                    //        if ((blinkerRectangle != null) && (blinkerRectangle.GuidID == notification.GuidID))
                    //        {
                    //            notifyRectangleToRemove = blinkerRectangle;
                    //            break;
                    //        }
                    //    }

                    //    if (notifyRectangleToRemove != null)
                    //    {
                    //        drawingCanvas.Children.Remove(notifyRectangleToRemove);
                    //    }
                    //}
                }
            }

        }

        public Guid PlanGuidId
        {
            get;
            private set;
        }

        public int PlanId
        {
            get;
            private set;
        }

        public Guid FolderGuid
        {
            get;
            private set;
        }

        public int VersionNbr
        {
            get;
            private set;
        }

        public PVDisplayStatus DisplayStatus
        {
            get;
            private set;
        }

        private NotifyType _notifyType;
        //ICIprivate DrawingCursor drawingCursor;
        public NotifyType NotifyType
        {
            get { return _notifyType; }
            set
            {
                if (_notifyType != value)
                {
                    _notifyType = value;
                    OnNotifyTypeChanged();
                }
            }
        }

        private FrameworkElement CurrentElement
        {
            get { return _currentElement; }
            set
            {
                if (_currentElement != value)
                {
                    _currentElement = value;
                    OnCurrentElementChanged();
                }
            }
        }



        protected void OnCurrentElementChanged()
        {
            if (((_previousSelectedElement == null) && (_currentElement != null))
                || ((_currentElement == null) && (_previousSelectedElement != null)))
            {
                if (IsElementSelectedChanged != null)
                    IsElementSelectedChanged(this, EventArgs.Empty);
            }

            bool isPreviousElementAText = (_previousSelectedElement is TextBox) || (_previousSelectedElement is TextBlock);
            bool isElementAText = (_currentElement is TextBox) || (_currentElement is TextBlock);

            if (isPreviousElementAText != isElementAText)
            {
                if (IsElementSelectedAtextChanged != null)
                    IsElementSelectedAtextChanged(this, EventArgs.Empty);
            }

            _previousSelectedElement = _currentElement;

            if (_currentElement != null)
            {
                Shape currentShape = _currentElement as Shape;
                if (currentShape != null)
                {
                    _notifyDrawBrushWidth = (int)currentShape.StrokeThickness;
                    if (NotifyDrawBrushWidthChanged != null)
                        NotifyDrawBrushWidthChanged(this, EventArgs.Empty);

                    notifyDrawBrushColor = (SolidColorBrush)currentShape.Stroke;
                    if (NotifyDrawBrushColorChanged != null)
                        NotifyDrawBrushColorChanged(this, EventArgs.Empty);

                    notifyLineFormatType = NoteShape.GetLineFormatTypeFromStrokeDashArray(currentShape.StrokeDashArray);
                    if (NotifyLineFormatTypeChanged != null)
                        NotifyLineFormatTypeChanged(this, EventArgs.Empty);

                    ShowCurrentShapeHandlers();
                }
                else
                {
                    TextBox currentTextBox = _currentElement as TextBox;
                    if (currentTextBox != null)
                    {
                        _notifyDrawBrushWidth = (int)currentTextBox.FontSize;
                        if (NotifyDrawBrushWidthChanged != null)
                            NotifyDrawBrushWidthChanged(this, EventArgs.Empty);

                        notifyDrawBrushColor = (SolidColorBrush)currentTextBox.Foreground;
                        if (NotifyDrawBrushColorChanged != null)
                            NotifyDrawBrushColorChanged(this, EventArgs.Empty);

                        notifyLineFormatType = NotifyLineFormatType.Full;
                        if (NotifyLineFormatTypeChanged != null)
                            NotifyLineFormatTypeChanged(this, EventArgs.Empty);

                        HideCurrentShapeHandlers();
                        HideCurrentShapeMover();
                    }

                }

                ShowCurrentShapeMover();
                ShowShapePinMover();
            }
            else
            {
                HideCurrentShapeHandlers();
                HideCurrentShapeMover();
            }
        }

        public void SelectDefaultPin()
        {
            if (_currentNotifyElementList != null && _currentNotifyElementList.Count > 0)
            {
                foreach (FrameworkElement noteShape in _currentNotifyElementList)
                {
                    ItemPin pin = noteShape as ItemPin;
                    if (pin != null)
                    {
                        currentItemPin = pin;
                        CurrentElement = currentItemPin;
                    }
                }
            }

        }


        private Ellipse _shapePinMover;
        private void ShowShapePinMover()
        {
            if (_currentElement == null || !editNote || drawingCanvas == null || _currentElement.Tag.ToString() != "Pin")
                return;

            double x2 = -1;
            double y2 = -1;
            double scale = GetCurrentScale();
            if (_shapePinMover == null)
            {
                _shapePinMover = new Ellipse();

                WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(_shapePinMover, this, (ls, s, e) => ls.shapeMover_MouseLeftButtonDown(s, e));
                WeakEventHelper.RegisterMouseLeftButtonUp<PictureViewer>(_shapePinMover, this, (ls, s, e) => ls.shapeMover_MouseLeftButtonUp(s, e));
                WeakEventHelper.RegisterMouseMove<PictureViewer>(_shapePinMover, this, (ls, s, e) => ls.shapePinMover_MouseMove(s, e));

                _shapePinMover.Cursor = Cursors.Hand;
                _shapePinMover.Fill = new SolidColorBrush(Colors.Transparent);
                //shapePinMover.Fill = new SolidColorBrush(Colors.Red);
            }
            if (!drawingCanvas.Children.Contains(_shapePinMover))
                drawingCanvas.Children.Add(_shapePinMover);
            _shapePinMover.Width = 78;
            _shapePinMover.Height = 78;
            ItemPin itemPin = null;
            if (_currentElement.Tag.ToString() == "Pin")
            {
                itemPin = (ItemPin)_currentElement;
                itemPin.Cursor = Cursors.Arrow;
                itemPin.UpdateLayout();

                GetShapeHandlerLocation(itemPin, out x2, out y2);
                y2 += itemPin.PixelHeight;
                x2 += itemPin.PixelWidth;
            }
            RotateMoverPin(itemPin);
        }

        private void ShowCurrentShapeMover()
        {
            if (_currentElement == null || _currentElement.Tag.ToString() == "Pin" || !editNote)
                return;


            double scale = GetCurrentScale();

            if (_shapeMover == null)
            {
                _shapeMover = new Rectangle();

                WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(_shapeMover, this, (ls, s, e) => ls.shapeMover_MouseLeftButtonDown(s, e));
                WeakEventHelper.RegisterMouseLeftButtonUp<PictureViewer>(_shapeMover, this, (ls, s, e) => ls.shapeMover_MouseLeftButtonUp(s, e));
                WeakEventHelper.RegisterMouseMove<PictureViewer>(_shapeMover, this, (ls, s, e) => ls.shapeMover_MouseMove(s, e));

                _shapeMover.Cursor = Cursors.Hand;

                //shapeMover.Stroke = new SolidColorBrush(Colors.Red);
                _shapeMover.Fill = new SolidColorBrush(Colors.White);

                drawingCanvas.Children.Add(_shapeMover);
            }
            _shapeMover.Stroke = notifyDrawBrushColor;
            _shapeMover.StrokeThickness = 2 / scale;
            _shapeMover.Width = 8 / scale;
            _shapeMover.Height = 8 / scale;

            //double x1 = -1;
            //double y1 = -1;
            double x2 = -1;
            double y2 = -1;

            if (_currentElement.Tag != null)
            {
                if (_currentElement.Tag.ToString() == "Line")
                {
                    Line line = (Line)_currentElement;
                    line.Cursor = Cursors.Arrow;
                    //x1 = line.X1;
                    //y1 = line.Y1;
                    x2 = line.X2;
                    y2 = line.Y2;
                }
                else if (_currentElement.Tag.ToString() == "Rectangle")
                {
                    Rectangle rect = (Rectangle)_currentElement;
                    rect.Cursor = Cursors.Arrow;
                    GetShapeHandlerLocation(rect, out x2, out y2);
                }
                else if (_currentElement.Tag.ToString() == "Ellipse")
                {
                    Ellipse ellipse = (Ellipse)_currentElement;
                    ellipse.Cursor = Cursors.Arrow;
                    GetShapeHandlerLocation(ellipse, out x2, out y2);

                }
                else if (_currentElement.Tag.ToString() == "Cloud")
                {
                    Path path = (Path)_currentElement;
                    path.Cursor = Cursors.Arrow;
                    GetShapeHandlerLocation(path, out x2, out y2);

                }
                else if (_currentElement.Tag.ToString() == "Polyline")
                {
                    Polyline polyline = (Polyline)_currentElement;
                    polyline.Cursor = Cursors.Arrow;
                    Point firstPoint = polyline.Points[0];
                    Point lastPoint = polyline.Points[polyline.Points.Count - 1];

                    x2 = lastPoint.X;
                    y2 = lastPoint.Y;
                }
                else if (_currentElement.Tag.ToString() == "TextBox")
                {
                    TextBox textBox = (TextBox)_currentElement;
                    textBox.UpdateLayout();

                    GetShapeHandlerLocation(textBox, out x2, out y2);
                }
                else if (_currentElement.Tag.ToString() == "TextBlock")
                {
                    TextBlock textBlock = (TextBlock)_currentElement;
                    textBlock.Cursor = Cursors.Arrow;
                    textBlock.UpdateLayout();

                    GetShapeHandlerLocation(textBlock, out x2, out y2);
                }
                else if (_currentElement.Tag.ToString() == "Pin")
                {
                    ItemPin itemPin = (ItemPin)_currentElement;
                    itemPin.Cursor = Cursors.Arrow;
                    itemPin.UpdateLayout();

                    GetShapeHandlerLocation(itemPin, out x2, out y2);
                    y2 += itemPin.PixelHeight;
                    x2 += itemPin.PixelWidth;


                    NoteShape notificationPin = GetNoteShape(CurrentDrawing, CurrentElement.Name);
                    NotePin pin = notificationPin as NotePin;
                    if (pin != null)
                    {
                        pin.X = Canvas.GetLeft(currentItemPin) + 7;
                        pin.Y = Canvas.GetTop(currentItemPin) + currentItemPin.PixelHeight - 4;
                    }
                }

                switch (_rotateAngle)
                {
                    case 0:
                        Canvas.SetLeft(_shapeMover, x2 + 8 / scale);
                        Canvas.SetTop(_shapeMover, y2 + 8 / scale);
                        break;

                    case -270:
                    case 90:
                        Canvas.SetLeft(_shapeMover, x2 + 8 / scale);
                        Canvas.SetTop(_shapeMover, y2 - 16 / scale);
                        break;

                    case -180:
                    case 180:
                        Canvas.SetLeft(_shapeMover, x2 - 16 / scale);
                        Canvas.SetTop(_shapeMover, y2 - 16 / scale);
                        break;

                    case -90:
                    case 270:
                        Canvas.SetLeft(_shapeMover, x2 - 16 / scale);
                        Canvas.SetTop(_shapeMover, y2 + 8 / scale);
                        break;
                }
            }
        }

        private void GetShapeHandlerLocation(FrameworkElement element, out double x, out double y)
        {
            int elementRotateAngle = 0;
            RotateTransform rotateTransform = element.RenderTransform as RotateTransform;
            if (rotateTransform != null)
            {
                elementRotateAngle = (int)rotateTransform.Angle;

            }

            x = 0;
            y = 0;
            switch (_rotateAngle)
            {
                case 0:
                    switch (elementRotateAngle)
                    {

                        case 0:
                            x = Canvas.GetLeft(element) + element.ActualWidth;
                            y = Canvas.GetTop(element) + element.ActualHeight;
                            break;

                        case 90:
                        case -270:
                            x = Canvas.GetLeft(element);
                            y = Canvas.GetTop(element) + element.ActualWidth;
                            break;

                        case 180:
                        case -180:
                            x = Canvas.GetLeft(element);
                            y = Canvas.GetTop(element);
                            break;

                        case -90:
                        case 270:
                            x = Canvas.GetLeft(element) + element.ActualHeight;
                            y = Canvas.GetTop(element);
                            break;
                    }
                    break;

                case 90:
                case -270:
                    switch (elementRotateAngle)
                    {
                        case 0:
                            x = Canvas.GetLeft(element) + element.ActualWidth;
                            y = Canvas.GetTop(element); // + element.ActualHeight;
                            break;

                        case 90:
                        case -270:
                            x = Canvas.GetLeft(element); // + element.ActualHeight;
                            y = Canvas.GetTop(element); // + element.ActualWidth;
                            break;

                        case 180:
                        case -180:
                            x = Canvas.GetLeft(element);
                            y = Canvas.GetTop(element) - element.ActualHeight;
                            break;

                        case -90:
                        case 270:
                            x = Canvas.GetLeft(element) + element.ActualHeight;
                            y = Canvas.GetTop(element) - element.ActualWidth;
                            break;
                    }
                    break;

                case 180:
                case -180:
                    switch (elementRotateAngle)
                    {
                        case 0:
                            x = Canvas.GetLeft(element);
                            y = Canvas.GetTop(element);
                            break;

                        case 90:
                        case -270:
                            x = Canvas.GetLeft(element) - element.ActualHeight;
                            y = Canvas.GetTop(element);
                            break;

                        case 180:
                        case -180:
                            x = Canvas.GetLeft(element) - element.ActualWidth;
                            y = Canvas.GetTop(element) - element.ActualHeight;

                            break;

                        case -90:
                        case 270:
                            x = Canvas.GetLeft(element);
                            y = Canvas.GetTop(element) - element.ActualWidth;

                            break;
                    }
                    break;

                case -90:
                case 270:
                    switch (elementRotateAngle)
                    {
                        case 0:
                            x = Canvas.GetLeft(element);
                            y = Canvas.GetTop(element) + element.ActualHeight;
                            break;

                        case 90:
                        case -270:
                            x = Canvas.GetLeft(element) - element.ActualHeight;
                            y = Canvas.GetTop(element) + element.ActualWidth;
                            break;

                        case 180:
                        case -180:
                            x = Canvas.GetLeft(element) - element.ActualWidth;
                            y = Canvas.GetTop(element);
                            break;

                        case -90:
                        case 270:
                            x = Canvas.GetLeft(element);
                            y = Canvas.GetTop(element);
                            break;
                    }
                    break;
            }

        }

        double drawingOpacity = 1;
        public double DrawingOpacity
        {
            get
            {
                if (this.NotifyType == ControlLibrary.NotifyType.Crop)
                    return 1;

                return drawingOpacity;
            }
            set
            {
                if (value != drawingOpacity)
                {
                    drawingOpacity = value;
                    OnDrawingOpacityChanged();
                    OnPropertyChanged("DrawingOpacity");
                }
            }
        }

        protected void OnDrawingOpacityChanged()
        {
            if (drawingCanvas != null)
                drawingCanvas.Opacity = DrawingOpacity;
        }

        private void HideCurrentShapeMover()
        {
            if (_shapeMover != null)
            {
                drawingCanvas.Children.Remove(_shapeMover);
                _shapeMover = null;
            }


        }

        void shapePinMover_MouseMove(object sender, MouseEventArgs e)
        {

            if (_shapeMoverIsInMove)
            {
                Point shapeMoverMouseLocation = new Point();
                double shapeMoverX = 0;
                double shapeMoverY = 0;

                shapeMoverMouseLocation = e.GetPosition(_shapePinMover);
                shapeMoverX = Canvas.GetLeft(_shapePinMover);
                shapeMoverY = Canvas.GetTop(_shapePinMover);
                double marginTop = currentItemPin.PixelHeight - 2;
                //shapeMoverMouseLocation.X = shapeMoverMouseLocation.X - 7 < 0 ? 0 - 7 : shapeMoverMouseLocation.X > pictureWidth ? pictureWidth - 7 : shapeMoverMouseLocation.X - 7;
                //shapeMoverMouseLocation.Y = shapeMoverMouseLocation.Y - marginTop < 0 ? 0 - marginTop : shapeMoverMouseLocation.Y - marginTop > pictureHeight ? pictureHeight - marginTop : shapeMoverMouseLocation.Y - marginTop;

                double deltaX = shapeMoverMouseLocation.X - _shapeMoverInitialMouseDownLocation.X;
                double deltaY = shapeMoverMouseLocation.Y - _shapeMoverInitialMouseDownLocation.Y;

                if (CurrentElement.Tag != null)
                {
                    NoteShape currentNoteShape = GetNoteShape(CurrentDrawing, CurrentElement.Name);
                    switch (CurrentElement.Tag.ToString())
                    {

                        case "Pin":
                            ItemPin itemPin = (ItemPin)CurrentElement;
                            double currentItemPinX = Canvas.GetLeft(CurrentElement);
                            double currentItemPinY = Canvas.GetTop(CurrentElement);
                            double newPositionX = currentItemPinX + deltaX;
                            double newPositionY = currentItemPinY + deltaY;
                            double limitWidth = 0;
                            double limitHeight = 0;
                            double marginLeft = 0;
                            switch (_rotateAngle)
                            {
                                case 0:
                                case 180:
                                    limitHeight = _pictureHeight;
                                    limitWidth = _pictureWidth;
                                    marginLeft = 7;
                                    marginTop = currentItemPin.PixelHeight - 2;
                                    break;
                                case 90:
                                case 270:
                                    marginLeft = currentItemPin.PixelHeight - 2;
                                    marginTop = 7;
                                    limitHeight = _pictureWidth;
                                    limitWidth = _pictureHeight;
                                    break;
                            }
                            double left = newPositionX + marginLeft < 0 ? 0 - marginLeft : newPositionX + marginLeft > limitWidth ? limitWidth - marginLeft : newPositionX;
                            double top = newPositionY + marginTop < 0 ? 0 - marginTop : newPositionY + marginTop > limitHeight ? limitHeight - marginTop : newPositionY;
                            //double top = newPositionY -marginTop < 0 ? 0 - marginTop : newPositionY marginTop > pictureHeight ? pictureHeight - currentItemPin.PixelHeight + 4 : newPositionY - currentItemPin.PixelHeight + 4;
                            Canvas.SetLeft(CurrentElement, left);
                            Canvas.SetTop(CurrentElement, top);
                            NotePin currentNotePin = (NotePin)currentNoteShape;
                            switch (_rotateAngle)
                            {
                                case 0:
                                    currentNotePin.X = left;
                                    currentNotePin.Y = top + 128;
                                    break;
                                case 90:
                                    currentNotePin.X = left + 128;
                                    currentNotePin.Y = top;
                                    break;
                                case 180:
                                    currentNotePin.X = left - 128;
                                    currentNotePin.Y = top - 128;
                                    break;
                                case 270:
                                    currentNotePin.X = left - 128;
                                    currentNotePin.Y = top;
                                    break;

                            }
                            RotateMoverPin(itemPin);
                            break;

                    }
                }
            }
        }

        void RotateMoverPin(ItemPin itemPin)
        {
            switch (_rotateAngle)
            {
                case 0:
                    Canvas.SetLeft(_shapePinMover, Canvas.GetLeft(itemPin) + 44);
                    Canvas.SetTop(_shapePinMover, Canvas.GetTop(itemPin));
                    break;
                case 90:
                    Canvas.SetLeft(_shapePinMover, Canvas.GetLeft(itemPin));
                    Canvas.SetTop(_shapePinMover, Canvas.GetTop(itemPin) - 121);
                    break;
                case 180:
                    Canvas.SetLeft(_shapePinMover, Canvas.GetLeft(itemPin) - 121);
                    Canvas.SetTop(_shapePinMover, Canvas.GetTop(itemPin) - 78);
                    break;
                case 270:
                    Canvas.SetLeft(_shapePinMover, Canvas.GetLeft(itemPin) - 78);
                    Canvas.SetTop(_shapePinMover, Canvas.GetTop(itemPin) + 44);
                    break;
            }
        }
        //--
        void shapeMover_MouseMove(object sender, MouseEventArgs e)
        {
            //ICIif (drawingCursor != null)
            //ICI    drawingCursor.Visibility = System.Windows.Visibility.Collapsed;
            //ICIthis.Cursor = Cursors.Hand;

            if (_shapeMoverIsInMove)
            {
                double shapeMoverX = 0;
                double shapeMoverY = 0;

                Point shapeMoverMouseLocation = e.GetPosition(_shapeMover);
                shapeMoverX = Canvas.GetLeft(_shapeMover);
                shapeMoverY = Canvas.GetTop(_shapeMover);
                double deltaX = shapeMoverMouseLocation.X - _shapeMoverInitialMouseDownLocation.X;

                double deltaY = shapeMoverMouseLocation.Y - _shapeMoverInitialMouseDownLocation.Y;


                if (CurrentElement.Tag != null)
                {
                    NoteShape currentNoteShape = GetNoteShape(CurrentDrawing, CurrentElement.Name);
                    Canvas.SetLeft(_shapeMover, shapeMoverX + deltaX);
                    Canvas.SetTop(_shapeMover, shapeMoverY + deltaY);

                    if (_shapeHandler1 != null)
                    {
                        double shapeHandler1X = Canvas.GetLeft(_shapeHandler1);
                        double shapeHandler1Y = Canvas.GetTop(_shapeHandler1);
                        Canvas.SetLeft(_shapeHandler1, shapeHandler1X + deltaX);
                        Canvas.SetTop(_shapeHandler1, shapeHandler1Y + deltaY);
                    }

                    if (_shapeHandler2 != null)
                    {
                        double shapeHandler2X = Canvas.GetLeft(_shapeHandler2);
                        double shapeHandler2Y = Canvas.GetTop(_shapeHandler2);
                        Canvas.SetLeft(_shapeHandler2, shapeHandler2X + deltaX);
                        Canvas.SetTop(_shapeHandler2, shapeHandler2Y + deltaY);
                    }

                    switch (CurrentElement.Tag.ToString())
                    {
                        case "Rectangle":
                        case "Ellipse":
                        case "Cloud":
                        case "TextBlock":
                        case "TextBox":
                            double currentElementX = Canvas.GetLeft(CurrentElement);
                            double currentElementY = Canvas.GetTop(CurrentElement);

                            Canvas.SetLeft(CurrentElement, currentElementX + deltaX);
                            Canvas.SetTop(CurrentElement, currentElementY + deltaY);

                            if ((CurrentElement.Tag.ToString() == "TextBlock") || (CurrentElement.Tag.ToString() == "TextBox"))
                            {
                                NoteText currentNoteText = (NoteText)currentNoteShape;
                                currentNoteText.X = currentElementX + deltaX;
                                currentNoteText.Y = currentElementY + deltaY;
                            }
                            else
                            {
                                NoteFlatShape currentNoteFlatShape = (NoteFlatShape)currentNoteShape;
                                currentNoteFlatShape.X = currentElementX + deltaX;
                                currentNoteFlatShape.Y = currentElementY + deltaY;
                            }
                            break;


                        case "Line":
                            Line line = (Line)CurrentElement;

                            line.X1 += deltaX;
                            line.Y1 += deltaY;
                            line.X2 += deltaX;
                            line.Y2 += deltaY;

                            NoteLine currentNoteLine = (NoteLine)currentNoteShape;
                            currentNoteLine.X1 += deltaX;
                            currentNoteLine.Y1 += deltaY;
                            currentNoteLine.X2 += deltaX;
                            currentNoteLine.Y2 += deltaY;

                            break;

                        case "Polyline":
                            Polyline polyline = (Polyline)CurrentElement;
                            for (int i = 0; i < polyline.Points.Count; i++)
                            {
                                double polylineX1 = polyline.Points[i].X;
                                double polylineY1 = polyline.Points[i].Y;

                                polylineX1 += deltaX;
                                polylineY1 += deltaY;

                                polyline.Points[i] = new Point(polylineX1, polylineY1);
                            }

                            NotePolyline currentNotePolyline = (NotePolyline)currentNoteShape;
                            for (int i = 0; i < currentNotePolyline.Points.Count; i++)
                            {
                                double polylineX1 = polyline.Points[i].X;
                                double polylineY1 = polyline.Points[i].Y;

                                polylineX1 += deltaX;
                                polylineY1 += deltaY;

                                currentNotePolyline.Points[i] = new Point(polylineX1, polylineY1);
                            }
                            break;
                        case "Pin":
                            double currentItemPinX = Canvas.GetLeft(CurrentElement);
                            double currentItemPinY = Canvas.GetTop(CurrentElement);

                            Canvas.SetLeft(CurrentElement, currentItemPinX + deltaX);
                            Canvas.SetTop(CurrentElement, currentItemPinY + deltaY);

                            NotePin currentNotePin = (NotePin)currentNoteShape;
                            currentNotePin.X += deltaX;
                            currentNotePin.Y += deltaY;
                            break;


                    }
                }
            }
        }

        void shapeMover_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _shapeMoverIsInMove = false;
            if (sender is Ellipse)
            {
                _shapePinMover.ReleaseMouseCapture();
            }
            else
            {
                _shapeMover.ReleaseMouseCapture();
            }
            _shapeMover.ReleaseMouseCapture();

            if (NoteDrawChanged != null)
                NoteDrawChanged(this, EventArgs.Empty);
            DisplayCursor(this.Cursor == Cursors.None, e.GetPosition(this));
        }

        private void DisplayCursor(bool visible = true, Point pt = default(Point))
        {
            if (visible)
            {
                _cursor.Visibility = System.Windows.Visibility.Visible;

                Canvas.SetLeft(_cursor, pt.X - _cursor.Width / 2);
                Canvas.SetTop(_cursor, pt.Y - _cursor.Height / 2);

            }
            else
            {
                _cursor.Visibility = System.Windows.Visibility.Collapsed;
            }
        }



        private bool _shapeMoverIsInMove;
        private Point _shapeMoverInitialMouseDownLocation;
        void shapeMover_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Ellipse)
            {
                if (_currentNotifyElementList != null && _currentElement == null)
                {
                    foreach (FrameworkElement el in _currentNotifyElementList)
                    {
                        if (el is ItemPin)
                            _currentElement = el;
                        break;
                    }
                }

                _shapeMoverInitialMouseDownLocation = e.GetPosition(_shapePinMover);
                _shapePinMover.CaptureMouse();
            }
            else
            {
                _shapeMoverInitialMouseDownLocation = e.GetPosition(_shapeMover);
                _shapeMover.CaptureMouse();
            }
            _shapeMoverIsInMove = true;
            DisplayCursor(false);
        }

        private Ellipse _shapeHandler1;
        private Ellipse _shapeHandler2;
        private void ShowCurrentShapeHandlers()
        {
            if (_currentElement == null || _currentElement.Tag.ToString() == "Pin" || !editNote)
                return;

            double scale = GetCurrentScale();

            if (_shapeHandler1 == null)
            {
                _shapeHandler1 = new Ellipse();

                WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(_shapeHandler1, this, (ls, s, e) => ls.shapeHandler1_MouseLeftButtonDown(s, e));
                WeakEventHelper.RegisterMouseLeftButtonUp<PictureViewer>(_shapeHandler1, this, (ls, s, e) => ls.shapeHandler1_MouseLeftButtonUp(s, e));
                WeakEventHelper.RegisterMouseMove<PictureViewer>(_shapeHandler1, this, (ls, s, e) => ls.shapeHandler1_MouseMove(s, e));

                _shapeHandler1.Cursor = Cursors.SizeNWSE;

                //shapeHandler1.Stroke = new SolidColorBrush(Colors.Red);
                _shapeHandler1.Fill = new SolidColorBrush(Colors.White);

                drawingCanvas.Children.Add(_shapeHandler1);
            }
            _shapeHandler1.Stroke = notifyDrawBrushColor;
            _shapeHandler1.StrokeThickness = 2 / scale;
            _shapeHandler1.Width = 8 / scale;
            _shapeHandler1.Height = 8 / scale;

            if (_shapeHandler2 == null)
            {
                _shapeHandler2 = new Ellipse();

                WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(_shapeHandler2, this, (ls, s, e) => ls.shapeHandler2_MouseLeftButtonDown(s, e));
                WeakEventHelper.RegisterMouseLeftButtonUp<PictureViewer>(_shapeHandler2, this, (ls, s, e) => ls.shapeHandler2_MouseLeftButtonUp(s, e));
                WeakEventHelper.RegisterMouseMove<PictureViewer>(_shapeHandler2, this, (ls, s, e) => ls.shapeHandler2_MouseMove(s, e));

                _shapeHandler2.Cursor = Cursors.SizeNWSE;

                // shapeHandler2.Stroke = new SolidColorBrush(Colors.Red);
                _shapeHandler2.Fill = new SolidColorBrush(Colors.White);

                drawingCanvas.Children.Add(_shapeHandler2);
            }
            _shapeHandler2.Stroke = notifyDrawBrushColor;
            _shapeHandler2.StrokeThickness = 2 / scale;
            _shapeHandler2.Width = 8 / scale;
            _shapeHandler2.Height = 8 / scale;

            double x1 = -1;
            double y1 = -1;
            double x2 = -1;
            double y2 = -1;

            if (_currentElement.Tag != null)
            {
                if (_currentElement.Tag.ToString() == "Line")
                {
                    Line line = (Line)_currentElement;
                    x1 = line.X1;
                    y1 = line.Y1;
                    x2 = line.X2;
                    y2 = line.Y2;
                }
                else if (_currentElement.Tag.ToString() == "Rectangle")
                {
                    Rectangle rect = (Rectangle)_currentElement;

                    if ((_rotateAngle == 0) || (_rotateAngle == 180))
                    {
                        x1 = Canvas.GetLeft(rect);
                        y1 = Canvas.GetTop(rect);
                        x2 = x1 + rect.ActualWidth;
                        y2 = y1 + rect.ActualHeight;
                    }
                    else
                    {
                        x1 = Canvas.GetLeft(rect) + rect.ActualWidth;
                        y1 = Canvas.GetTop(rect);
                        x2 = Canvas.GetLeft(rect);
                        y2 = y1 + rect.ActualHeight;
                    }
                }
                else if (_currentElement.Tag.ToString() == "Ellipse")
                {
                    Ellipse ellipse = (Ellipse)_currentElement;

                    if ((_rotateAngle == 0) || (_rotateAngle == 180))
                    {
                        x1 = Canvas.GetLeft(ellipse);
                        y1 = Canvas.GetTop(ellipse);
                        x2 = x1 + ellipse.ActualWidth;
                        y2 = y1 + ellipse.ActualHeight;
                    }
                    else
                    {
                        x1 = Canvas.GetLeft(ellipse) + ellipse.ActualWidth;
                        y1 = Canvas.GetTop(ellipse);
                        x2 = Canvas.GetLeft(ellipse);
                        y2 = y1 + ellipse.ActualHeight;
                    }

                }
                else if (_currentElement.Tag.ToString() == "Cloud")
                {
                    Path path = (Path)_currentElement;

                    if ((_rotateAngle == 0) || (_rotateAngle == 180))
                    {
                        x1 = Canvas.GetLeft(path);
                        y1 = Canvas.GetTop(path);
                        x2 = x1 + path.ActualWidth;
                        y2 = y1 + path.ActualHeight;
                    }
                    else
                    {
                        x1 = Canvas.GetLeft(path) + path.ActualWidth;
                        y1 = Canvas.GetTop(path);
                        x2 = Canvas.GetLeft(path);
                        y2 = y1 + path.ActualHeight;
                    }
                }
                else if (_currentElement.Tag.ToString() == "Polyline")
                {
                    Polyline polyline = (Polyline)_currentElement;
                    Point firstPoint = polyline.Points[0];
                    Point lastPoint = polyline.Points[polyline.Points.Count - 1];

                    x1 = firstPoint.X;
                    y1 = firstPoint.Y;
                    x2 = lastPoint.X;
                    y2 = lastPoint.Y;
                }
                else if (_currentElement.Tag.ToString() == "Pin")
                {
                    ItemPin itemPin = (ItemPin)_currentElement;
                    x1 = Canvas.GetLeft(itemPin);
                    y1 = Canvas.GetTop(itemPin);
                    x2 = x1 + itemPin.ActualWidth;
                    y2 = y1 + itemPin.ActualHeight;
                }


                Canvas.SetLeft(_shapeHandler1, x1 - 4 / scale);
                Canvas.SetTop(_shapeHandler1, y1 - 4 / scale);

                Canvas.SetLeft(_shapeHandler2, x2 - 4 / scale);
                Canvas.SetTop(_shapeHandler2, y2 - 4 / scale);

                Shape shape = (_currentElement as Shape);
                if (shape != null)
                {
                    //this.NotifyLineFormatType = ControlLibrary.NotifyLineFormatType.Dash;
                    shape.StrokeDashArray = NoteShape.GetStrokeDashByLineFormat(this.NotifyLineFormatType);
                }
            }
        }

        //void shapeHandler1_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    if (drawingCursor != null)
        //        drawingCursor.Visibility = System.Windows.Visibility.Collapsed;
        //    this.Cursor = Cursors.None;
        //}

        //void shapeHandler1_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    if (drawingCursor != null)
        //        drawingCursor.Visibility = System.Windows.Visibility.Collapsed;
        //    this.Cursor = Cursors.Hand;
        //}

        void shapeHandler1_MouseMove(object sender, MouseEventArgs e)
        {
            //ICIif (drawingCursor != null)
            //ICI    drawingCursor.Visibility = System.Windows.Visibility.Collapsed;
            //ICIthis.Cursor = Cursors.Hand;

            if (_shapeHandler1IsInMove)
            {
                Point shapeHandler1MouseLocation = e.GetPosition(_shapeHandler1);

                double shapeHandler1X = Canvas.GetLeft(_shapeHandler1);
                double deltaX = shapeHandler1MouseLocation.X - _shapeHandler1InitialMouseDownLocation.X;


                double shapeHandler1Y = Canvas.GetTop(_shapeHandler1);
                double deltaY = shapeHandler1MouseLocation.Y - _shapeHandler1InitialMouseDownLocation.Y;

                double shapeMoverX = Canvas.GetLeft(_shapeMover);
                double shapeMoverY = Canvas.GetTop(_shapeMover);

                if (CurrentElement.Tag != null)
                {
                    NoteShape currentNoteShape = GetNoteShape(CurrentDrawing, CurrentElement.Name);
                    switch (CurrentElement.Tag.ToString())
                    {
                        case "Rectangle":
                        case "Ellipse":
                        case "Cloud":
                            NoteFlatShape currentNoteFlatShap = (NoteFlatShape)currentNoteShape;

                            if ((_rotateAngle == 0) || (_rotateAngle == 180))
                            {
                                if (CurrentElement.Width > deltaX)
                                {
                                    Canvas.SetLeft(_shapeHandler1, shapeHandler1X + deltaX);
                                    double shapeX = Canvas.GetLeft(CurrentElement);
                                    Canvas.SetLeft(CurrentElement, shapeX + deltaX);
                                    CurrentElement.Width -= deltaX;

                                    currentNoteFlatShap.X = shapeX + deltaX;
                                    currentNoteFlatShap.Width -= deltaX;
                                }
                            }
                            else
                            {
                                if (CurrentElement.Width + deltaX > 0)
                                {
                                    Canvas.SetLeft(_shapeHandler1, shapeHandler1X + deltaX);
                                    CurrentElement.Width += deltaX;
                                    currentNoteFlatShap.Width += deltaX;
                                }
                            }


                            if (CurrentElement.Height > deltaY)
                            {
                                Canvas.SetTop(_shapeHandler1, shapeHandler1Y + deltaY);

                                double shapeY = Canvas.GetTop(CurrentElement);
                                Canvas.SetTop(CurrentElement, shapeY + deltaY);
                                CurrentElement.Height -= deltaY;

                                currentNoteFlatShap.Y = shapeY + deltaY;
                                currentNoteFlatShap.Height -= deltaY;
                            }

                            switch (_rotateAngle)
                            {
                                case 90:
                                    if (CurrentElement.Height > deltaY)
                                    {
                                        Canvas.SetTop(_shapeMover, shapeMoverY + deltaY);
                                    }

                                    if (CurrentElement.Width + deltaX > 0)
                                    {
                                        Canvas.SetLeft(_shapeMover, shapeMoverX + deltaX);
                                    }
                                    break;

                                case 180:
                                    if (CurrentElement.Height > deltaY)
                                    {
                                        Canvas.SetTop(_shapeMover, shapeMoverY + deltaY);
                                    }

                                    if (CurrentElement.Width > deltaX)
                                    {
                                        Canvas.SetLeft(_shapeMover, shapeMoverX + deltaX);
                                    }
                                    break;
                            }

                            break;

                        case "Line":
                            NoteLine currentNoteLine = (NoteLine)currentNoteShape;
                            Line line = (Line)CurrentElement;
                            Canvas.SetLeft(_shapeHandler1, shapeHandler1X + deltaX);
                            line.X1 += deltaX;
                            currentNoteLine.X1 += deltaX;
                            Canvas.SetTop(_shapeHandler1, shapeHandler1Y + deltaY);
                            line.Y1 += deltaY;
                            currentNoteLine.Y1 += deltaY;
                            break;

                        case "Polyline":
                            NotePolyline currentNotePolyline = (NotePolyline)currentNoteShape;

                            Polyline polyline = (Polyline)CurrentElement;
                            double polylineX1 = polyline.Points[0].X;
                            double polylineY1 = polyline.Points[0].Y;

                            Canvas.SetLeft(_shapeHandler1, shapeHandler1X + deltaX);
                            polylineX1 += deltaX;
                            Canvas.SetTop(_shapeHandler1, shapeHandler1Y + deltaY);
                            polylineY1 += deltaY;

                            polyline.Points[0] = new Point(polylineX1, polylineY1);
                            currentNotePolyline.Points[0] = new Point(polylineX1, polylineY1);
                            break;
                        case "Pin":
                            ResizePin(currentNoteShape, deltaX, deltaY, shapeHandler1X, shapeHandler1Y, shapeMoverX, shapeMoverY);
                            break;
                    }
                }
            }
        }

        void ResizePin(NoteShape currentNoteShape, double deltaX, double deltaY, double shapeHandler1X, double shapeHandler1Y, double shapeMoverX, double shapeMoverY)
        {
            NotePin currentNotePin = (NotePin)currentNoteShape;

            if ((_rotateAngle == 0) || (_rotateAngle == 180))
            {
                if (CurrentElement.Width > deltaX)
                {
                    Canvas.SetLeft(_shapeHandler1, shapeHandler1X + deltaX);
                    double shapeX = Canvas.GetLeft(CurrentElement);
                    Canvas.SetLeft(CurrentElement, shapeX + deltaX);
                    CurrentElement.Width -= deltaX;

                    currentNotePin.X = shapeX + deltaX;
                    //currentNotePin.Width -= deltaX;
                }
            }
            else
            {
                if (CurrentElement.Width + deltaX > 0)
                {
                    Canvas.SetLeft(_shapeHandler1, shapeHandler1X + deltaX);
                    CurrentElement.Width += deltaX;
                    //currentNotePin.Width += deltaX;
                }
            }


            if (CurrentElement.Height > deltaY)
            {
                Canvas.SetTop(_shapeHandler1, shapeHandler1Y + deltaY);

                double shapeY = Canvas.GetTop(CurrentElement);
                Canvas.SetTop(CurrentElement, shapeY + deltaY);
                CurrentElement.Height -= deltaY;

                currentNotePin.Y = shapeY + deltaY;
                //currentNotePin.Height -= deltaY;
            }

            switch (_rotateAngle)
            {
                case 90:
                    if (CurrentElement.Height > deltaY)
                    {
                        Canvas.SetTop(_shapeMover, shapeMoverY + deltaY);
                    }

                    if (CurrentElement.Width + deltaX > 0)
                    {
                        Canvas.SetLeft(_shapeMover, shapeMoverX + deltaX);
                    }
                    break;

                case 180:
                    if (CurrentElement.Height > deltaY)
                    {
                        Canvas.SetTop(_shapeMover, shapeMoverY + deltaY);
                    }

                    if (CurrentElement.Width > deltaX)
                    {
                        Canvas.SetLeft(_shapeMover, shapeMoverX + deltaX);
                    }
                    break;
            }
        }


        void shapeHandler1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _shapeHandler1IsInMove = false;
            _shapeHandler1.ReleaseMouseCapture();
            DisplayCursor(this.Cursor == Cursors.None, e.GetPosition(this));
            if (NoteDrawChanged != null)
                NoteDrawChanged(this, EventArgs.Empty);
        }



        private bool _shapeHandler1IsInMove;
        private Point _shapeHandler1InitialMouseDownLocation;
        void shapeHandler1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _shapeHandler1InitialMouseDownLocation = e.GetPosition(_shapeHandler1);
            _shapeHandler1.CaptureMouse();
            _shapeHandler1IsInMove = true;

            DisplayCursor(false);
        }

        void shapeHandler2_MouseMove(object sender, MouseEventArgs e)
        {
            if (shapeHandler2IsInMove)
            {
                Point shapeHandler2MouseLocation = e.GetPosition(_shapeHandler2);

                double shapeHandler2X = Canvas.GetLeft(_shapeHandler2);
                double deltaX = shapeHandler2MouseLocation.X - shapeHandler2InitialMouseDownLocation.X;


                double shapeHandler2Y = Canvas.GetTop(_shapeHandler2);
                double deltaY = shapeHandler2MouseLocation.Y - shapeHandler2InitialMouseDownLocation.Y;

                double shapeMoverX = Canvas.GetLeft(_shapeMover);
                double shapeMoverY = Canvas.GetTop(_shapeMover);

                if (CurrentElement.Tag != null)
                {
                    NoteShape currentNoteShape = GetNoteShape(CurrentDrawing, CurrentElement.Name);
                    switch (CurrentElement.Tag.ToString())
                    {
                        case "Rectangle":
                        case "Ellipse":
                        case "Cloud":
                            NoteFlatShape currentNoteFlatShap = (NoteFlatShape)currentNoteShape;
                            if ((_rotateAngle == 0) || (_rotateAngle == 180))
                            {
                                if (CurrentElement.Width + deltaX > 0)
                                {
                                    Canvas.SetLeft(_shapeHandler2, shapeHandler2X + deltaX);
                                    CurrentElement.Width += deltaX;
                                    currentNoteFlatShap.Width += deltaX;

                                }
                            }
                            else
                            {
                                if (CurrentElement.Width > deltaX)
                                {
                                    Canvas.SetLeft(_shapeHandler2, shapeHandler2X + deltaX);
                                    double shapeX = Canvas.GetLeft(CurrentElement);
                                    Canvas.SetLeft(CurrentElement, shapeX + deltaX);
                                    CurrentElement.Width -= deltaX;

                                    currentNoteFlatShap.X = shapeX + deltaX;
                                    currentNoteFlatShap.Width -= deltaX;
                                }
                            }

                            if (CurrentElement.Height + deltaY > 0)
                            {
                                Canvas.SetTop(_shapeHandler2, shapeHandler2Y + deltaY);

                                CurrentElement.Height += deltaY;
                                currentNoteFlatShap.Height += deltaY;
                            }

                            switch (_rotateAngle)
                            {
                                case 0:
                                    if (CurrentElement.Height + deltaY > 0)
                                    {
                                        Canvas.SetTop(_shapeMover, shapeMoverY + deltaY);
                                    }

                                    if (CurrentElement.Width + deltaX > 0)
                                    {
                                        Canvas.SetLeft(_shapeMover, shapeMoverX + deltaX);
                                    }
                                    break;

                                case 270:
                                    if (CurrentElement.Height + deltaY > 0)
                                    {
                                        Canvas.SetTop(_shapeMover, shapeMoverY + deltaY);
                                    }

                                    if (CurrentElement.Width > deltaX)
                                    {
                                        Canvas.SetLeft(_shapeMover, shapeMoverX + deltaX);
                                    }

                                    break;
                            }

                            break;

                        case "Line":
                            NoteLine currentNoteLine = (NoteLine)currentNoteShape;
                            Line line = (Line)CurrentElement;
                            Canvas.SetLeft(_shapeHandler2, shapeHandler2X + deltaX);
                            Canvas.SetLeft(_shapeMover, shapeMoverX + deltaX);
                            line.X2 += deltaX;
                            currentNoteLine.X2 += deltaX;
                            Canvas.SetTop(_shapeHandler2, shapeHandler2Y + deltaY);
                            Canvas.SetTop(_shapeMover, shapeMoverY + deltaY);
                            line.Y2 += deltaY;
                            currentNoteLine.Y2 += deltaY;


                            break;

                        case "Polyline":
                            NotePolyline currentNotePolyline = (NotePolyline)currentNoteShape;
                            double currentNotePolylineX2 = currentNotePolyline.Points[currentNotePolyline.Points.Count - 1].X;
                            double currentNotePolylineY2 = currentNotePolyline.Points[currentNotePolyline.Points.Count - 1].Y;

                            Polyline polyline = (Polyline)CurrentElement;
                            double polylineX2 = polyline.Points[polyline.Points.Count - 1].X;
                            double polylineY2 = polyline.Points[polyline.Points.Count - 1].Y;

                            Canvas.SetLeft(_shapeHandler2, shapeHandler2X + deltaX);
                            Canvas.SetLeft(_shapeMover, shapeMoverX + deltaX);
                            polylineX2 += deltaX;
                            currentNotePolylineX2 += deltaX;
                            Canvas.SetTop(_shapeHandler2, shapeHandler2Y + deltaY);
                            Canvas.SetTop(_shapeMover, shapeMoverY + deltaY);
                            polylineY2 += deltaY;
                            currentNotePolylineY2 += deltaY;

                            polyline.Points[polyline.Points.Count - 1] = new Point(polylineX2, polylineY2);
                            break;
                    }
                }
            }
        }

        void shapeHandler2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            shapeHandler2IsInMove = false;
            _shapeHandler2.ReleaseMouseCapture();
            DisplayCursor(this.Cursor == Cursors.None, e.GetPosition(this));
            if (NoteDrawChanged != null)
                NoteDrawChanged(this, EventArgs.Empty);
        }

        private bool shapeHandler2IsInMove;
        private Point shapeHandler2InitialMouseDownLocation;
        void shapeHandler2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            shapeHandler2InitialMouseDownLocation = e.GetPosition(_shapeHandler2);
            _shapeHandler2.CaptureMouse();

            shapeHandler2IsInMove = true;
            DisplayCursor(false);
        }

        private void HideCurrentShapeHandlers()
        {
            if (_shapeHandler1 != null)
            {
                drawingCanvas.Children.Remove(_shapeHandler1);
                _shapeHandler1 = null;
            }

            if (_shapeHandler2 != null)
            {
                drawingCanvas.Children.Remove(_shapeHandler2);
                _shapeHandler2 = null;
            }
        }

        public bool IsShapeSelected
        {
            get { return _currentElement != null; }
        }

        public bool IsShapeSelectedAText
        {
            get { return ((_currentElement as TextBox) != null) || ((_currentElement as TextBlock) != null); }
        }

        public void DeleteCurrentShape()
        {
            if (CurrentElement == null)
                return;

            CurrentElement.MouseLeftButtonDown -= new MouseButtonEventHandler(currentShape_MouseLeftButtonDown);
            TextBox currentTextBox = CurrentElement as TextBox;
            if (currentTextBox != null)
            {
                currentTextBox.TextChanged -= new TextChangedEventHandler(textBox_TextChanged);
            }
            CurrentNotifyShapeList.Remove(CurrentElement);

            NoteShape notificationShape = GetNoteShape(CurrentDrawing, CurrentElement.Name);
            CurrentDrawing.Shapes.Remove(notificationShape);

            CurrentElement = null;

            if (NoteDrawChanged != null)
                NoteDrawChanged(this, EventArgs.Empty);
        }

        public void ClearDrawings()
        {
            if (CurrentNotifyRectangle != null)
            {
                CurrentNotifyRectangle.Dispose();
            }
            CurrentNotifyRectangle = null;
            if (drawingCanvas != null)
            {
                drawingCanvas.Children.Clear();
                _shapeMover = null;
                _shapeHandler1 = null;
                _shapeHandler2 = null;
                ClearCurrentNotifShapeList();

            }
        }

        protected void OnNotifyTypeChanged()
        {
            if (this.Visibility == System.Windows.Visibility.Collapsed)
                return;

            OnDrawingOpacityChanged();

            if (CurrentElement != null)
            {
                UnselectCurrentElement();

                CurrentElement = null;
            }

            if ((NotifyType == ControlLibrary.NotifyType.None) || (NotifyType == MaVuViecClient.NotifyType.Draw))
            {
                this.Cursor = System.Windows.Input.Cursors.Hand;
            }
            else if (NotifyType == MaVuViecClient.NotifyType.DrawText)
            {
                this.Cursor = System.Windows.Input.Cursors.IBeam;
            }
            else
            {
                //this.Cursor = System.Windows.Input.Cursors.Stylus;
                this.Cursor = Cursors.None;
            }

        }

        private NotifyLineFormatType notifyLineFormatType;
        public event EventHandler NotifyLineFormatTypeChanged;
        public NotifyLineFormatType NotifyLineFormatType
        {
            get
            {

                return notifyLineFormatType;
            }

            set
            {
                if (notifyLineFormatType != value)
                {
                    notifyLineFormatType = value;
                    OnNotifyLineFormatTypeChanged();
                }
            }
        }

        protected void OnNotifyLineFormatTypeChanged()
        {
            if (CurrentElement != null)
            {
                Shape currentShape = CurrentElement as Shape;
                if (currentShape != null)
                {
                    currentShape.StrokeDashArray = NoteShape.GetStrokeDashByLineFormat(this.NotifyLineFormatType);
                    currentShape.UpdateLayout();
                }

                if (NoteDrawChanged != null)
                    NoteDrawChanged(this, EventArgs.Empty);
            }
        }


        public int NotifyDrawBrushWidth
        {
            get { return _notifyDrawBrushWidth; }
            set
            {
                if (_notifyDrawBrushWidth != value)
                {
                    _notifyDrawBrushWidth = value;
                    OnNotifyDrawBrishWidthChanged();
                }
            }
        }

        private void OnNotifyDrawBrishWidthChanged()
        {
            if (CurrentElement != null)
            {
                Shape currentShape = CurrentElement as Shape;
                if (currentShape != null)
                {
                    currentShape.StrokeThickness = (int)(NotifyDrawBrushWidth);
                    currentShape.UpdateLayout();
                    currentShape.StrokeStartLineCap = PenLineCap.Round;
                    currentShape.StrokeStartLineCap = PenLineCap.Flat; //to force the update of the display
                }
                else
                {
                    TextBox currentTextBox = CurrentElement as TextBox;
                    if (currentTextBox != null)
                    {
                        currentTextBox.FontSize = (int)(NotifyDrawBrushWidth);
                    }
                    else
                    {
                        TextBlock currentTextBlock = CurrentElement as TextBlock;
                        if (currentTextBlock != null)
                        {
                            currentTextBlock.FontSize = (int)(NotifyDrawBrushWidth);
                        }
                    }
                }

                NoteShape notificationShape = GetNoteShape(CurrentDrawing, CurrentElement.Name);
                notificationShape.DrawingSize = (int)(NotifyDrawBrushWidth);

                if (NoteDrawChanged != null)
                    NoteDrawChanged(this, EventArgs.Empty);
            }
        }

        //--
        private SolidColorBrush notifyDrawBrushColor = new SolidColorBrush(Colors.Red);
        public event EventHandler NotifyDrawBrushColorChanged;
        public SolidColorBrush NotifyDrawBrushColor
        {
            get { return notifyDrawBrushColor; }
            set
            {
                if (notifyDrawBrushColor != value)
                {
                    notifyDrawBrushColor = value;
                    OnNotifyDrawBrushColorChanged();
                    _cursor.Fill = notifyDrawBrushColor;


                    ShowCurrentShapeHandlers();

                }
            }
        }

        private void OnNotifyDrawBrushColorChanged()
        {
            if (CurrentElement != null)
            {
                Shape currentShape = CurrentElement as Shape;
                if (currentShape != null)
                {
                    currentShape.Stroke = notifyDrawBrushColor;
                    //CurrentShape.UpdateLayout();
                    //CurrentShape.StrokeStartLineCap = PenLineCap.Round;
                    //CurrentShape.StrokeStartLineCap = PenLineCap.Flat; //to force the update of the display
                }
                else
                {
                    TextBox currentTextBox = CurrentElement as TextBox;
                    if (currentTextBox != null)
                    {
                        currentTextBox.Foreground = notifyDrawBrushColor;
                    }
                    else
                    {
                        TextBlock currentTextBlock = CurrentElement as TextBlock;
                        if (currentTextBlock != null)
                        {
                            currentTextBlock.Foreground = notifyDrawBrushColor;
                        }
                    }
                }

                NoteShape notificationShape = GetNoteShape(CurrentDrawing, CurrentElement.Name);
                notificationShape.Color = notifyDrawBrushColor.Color;

                if (NoteDrawChanged != null)
                    NoteDrawChanged(this, EventArgs.Empty);
            }
        }

        private bool displayNote = false;
        public bool DisplayNote
        {
            get { return displayNote; }
            set
            {
                if (displayNote != value)
                {
                    displayNote = value;
                    OnDisplayNoteChanged();
                    UpdateDrawingCanvas();
                }
            }
        }

        protected void OnDisplayNoteChanged()
        {
            //if (gridOptionsBtn != null)
            //    gridOptionsBtn.IsEnabled = !displayNote && isAPlanShown;

            RefreshCurrentNoteDisplay();
        }

        private bool editNote = false;
        public bool EditNote
        {
            get { return editNote; }
            set
            {
                if (editNote != value)
                {
                    editNote = value;
                    OnEditNoteChanged();
                }
            }
        }

        protected void OnEditNoteChanged()
        {
            if (gridOptionsBtn != null)
                gridOptionsBtn.IsEnabled = !editNote && isAPlanShown;


            foreach (FrameworkElement element in _currentNotifyElementList)
            {
                if (editNote)
                {
                    element.Cursor = Cursors.Arrow;
                }
                else
                {
                    element.Cursor = Cursors.Hand;


                }
            }

            if (!editNote)
            {

                if (_shapeMover != null)
                {
                    drawingCanvas.Children.Remove(_shapeMover);
                    _shapeMover = null;
                }

                if (_shapeHandler1 != null)
                {
                    drawingCanvas.Children.Remove(_shapeHandler1);
                    _shapeHandler1 = null;
                }

                if (_shapeHandler2 != null)
                {
                    drawingCanvas.Children.Remove(_shapeHandler2);
                    _shapeHandler2 = null;
                }

                _currentElement = null;
            }

        }

        //public ObservableCollection<Note> NoteList
        //{
        //    get
        //    {
        //        return notesList;
        //    }
        //}

        public ObservableCollection<DocLink> LinksList
        {
            get
            {
                return _linksList;
            }
        }

        void gridOptions_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Color":
                case "Opacity":
                    UpdateGridColor();
                    break;

                case "CellSize":
                    DisposeGrid();
                    InitializeGrid();
                    UpdateGrid();
                    break;

                case "OriginX":
                case "OriginY":
                    UpdateGrid();
                    break;
            }
        }

        private void UpdateGridColor()
        {
            if (this.Visibility == System.Windows.Visibility.Collapsed)
                return;

            origineXLine.Stroke = new SolidColorBrush(ColorHelper.IntToColorWithoutAlpha(gridOptions.ColorWithoutAlpha));
            origineYLine.Stroke = new SolidColorBrush(ColorHelper.IntToColorWithoutAlpha(gridOptions.ColorWithoutAlpha));
            foreach (UIElement element in gridCanvas.Children)
            {
                Rectangle r = element as Rectangle;
                if (r != null)
                {
                    r.Fill = new SolidColorBrush(ColorHelper.IntToColorWithAlpha(gridOptions.ColorWithAlpha));
                }
            }
        }

        private Canvas imagesCanvas;
        private Canvas compareCanvas;
        private Image compareImage;
        private Canvas gridCanvas;
        private Canvas gridParent;
        private Canvas colHeaders;
        private Canvas rowHeaders;
        private Canvas drawingCanvas;
        private Canvas borderCanvas;
        private Canvas mouseCanvas;
        private Canvas linksCanvas;

        private Canvas highlightCanvas;

        private ToggleButton gridOptionsBtn;
        //private Border optionsBorder;
        private bool isTemplateApplied;

        private Canvas selectedAreaCanvas;

        private Storyboard higlightBoard;

        private Grid layoutRoot;

        private Storyboard blinkCompareCanvasSB;
        private Grid errorLoadingTilesGrid;
        private TextBlock errorLoadingTilesText;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.imagesCanvas = this.GetTemplateChild("ImagesCanvas") as Canvas;
            this.compareCanvas = this.GetTemplateChild("CompareCanvas") as Canvas;
            this.compareImage = this.GetTemplateChild("CompareImage") as Image;
            this.gridCanvas = this.GetTemplateChild("GridCanvas") as Canvas;
            this.gridParent = this.GetTemplateChild("GridParent") as Canvas;
            this.colHeaders = this.GetTemplateChild("ColHeaders") as Canvas;
            this.rowHeaders = this.GetTemplateChild("RowHeaders") as Canvas;
            this.linksCanvas = this.GetTemplateChild("LinksCanvas") as Canvas;
            this.drawingCanvas = this.GetTemplateChild("DrawingCanvas") as Canvas;
            this.mouseCanvas = this.GetTemplateChild("MouseCanvas") as Canvas;
            this.borderCanvas = this.GetTemplateChild("BorderCanvas") as Canvas;
            this.errorLoadingTilesGrid = this.GetTemplateChild("ErrorLoadingTilesGrid") as Grid;
            this.errorLoadingTilesText = this.GetTemplateChild("ErrorLoadingTilesText") as TextBlock;


            if (errorLoadingTilesText != null)
            {
                errorLoadingTilesText.Text = Global.Instance.LanguageDictionary["PictureViewer_GetTilesErrorMessage"];
            }
            UpdateErrorLoadingTiles();

            this.highlightCanvas = this.GetTemplateChild("HighlightCanvas") as Canvas;
            higlightBoard = new Storyboard();
            //JCA 25/11/11 this.highlightCanvas.Background = new SolidColorBrush(Colors.Transparent);  -> cannot do that because mouse events on other canvas will not be trapped after this change

            selectedAreaCanvas = this.GetTemplateChild("SelectedAreaCanvas") as Canvas;

            this._pinImage = this.GetTemplateChild("PinImage") as Image;

            //this.optionsBorder = this.GetTemplateChild("OptionsBorder") as Border;
            this.gridOptionsBtn = this.GetTemplateChild("GridOptionsBtn") as ToggleButton;

            this.layoutRoot = this.GetTemplateChild("LayoutRoot") as Grid;
            blinkCompareCanvasSB = layoutRoot.Resources["BlinkCompareCanvasSB"] as Storyboard;


#if PROTOTYPE           
            WeakEventHelper.RegisterToggleButtonChecked<PictureViewer>(gridOptionsBtn, this, (ls, s, e) => ls.gridOptionsBtn_Checked(s, e));
            WeakEventHelper.RegisterToggleButtonUnChecked<PictureViewer>(gridOptionsBtn, this, (ls, s, e) => ls.gridOptionsBtn_Unchecked(s, e));
#else
            gridOptionsBtn.Visibility = Visibility.Collapsed;
#endif
            isTemplateApplied = true;
            // custom cursor
            _cursor.SetBinding(Ellipse.FillProperty, new System.Windows.Data.Binding("NotifyDrawBrushColor") { Source = this, Mode = System.Windows.Data.BindingMode.OneWay });

            _cursor.SetBinding(Ellipse.OpacityProperty, new System.Windows.Data.Binding("DrawingOpacity") { Source = this, Mode = System.Windows.Data.BindingMode.OneWay });
            mouseCanvas.Children.Add(_cursor);
            if (drawingCanvas != null)
                drawingCanvas.Opacity = DrawingOpacity;

            UpdateTiles(this.ActualWidth, this.ActualHeight);

            RefreshCurrentNoteDisplay();
        }

        bool isGridOptionVisible = false;
        public event EventHandler GridOptionsUnChecked;
        void gridOptionsBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            isGridOptionVisible = false;
            if (origineXLine != null)
                origineXLine.Visibility = System.Windows.Visibility.Collapsed;

            if (origineYLine != null)
                origineYLine.Visibility = System.Windows.Visibility.Collapsed;

            if (GridOptionsUnChecked != null)
                GridOptionsUnChecked(this, EventArgs.Empty);
        }

        public event EventHandler GridOptionsChecked;
        void gridOptionsBtn_Checked(object sender, RoutedEventArgs e)
        {
            isGridOptionVisible = true;
            if (origineXLine != null)
                origineXLine.Visibility = System.Windows.Visibility.Visible;


            if (origineYLine != null)
                origineYLine.Visibility = System.Windows.Visibility.Visible;

            if (GridOptionsChecked != null)
                GridOptionsChecked(this, EventArgs.Empty);
        }

        public void Clear()
        {
            _pageIndex = 0;
            DisposeTiles();
            DisposeNotes();
            UpdateTiles(this.ActualWidth, this.ActualHeight);
            UpdateLayout();

            HiddenMessageSearchIndex(true);
        }

        private void DisposeTiles()
        {
            isAPlanShown = false;
            this.Cursor = System.Windows.Input.Cursors.Arrow;
            PlanId = -1;
            _renderLevel = -1;
            foreach (Layer l in _layers)
            {
                Tile[] tiles = l.Tiles;
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i] != null)
                    {
                        imagesCanvas.Children.Remove(tiles[i].Image);

                        tiles[i].Image = null;
                        tiles[i] = null;
                    }
                }
            }

            _layers.Clear();

            foreach (Tile tile in _tilesList)
            {
                tile.Image = null;
            }
            _tilesList.Clear();

            DisposeGrid();

            if (gridOptions != null)
            {
                gridOptions.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(gridOptions_PropertyChanged);
                gridOptions = null;
                if (gridOptionsBtn != null)
                    gridOptionsBtn.IsEnabled = false;
            }

            if (compareCanvas != null)
            {
                compareCanvas.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (blinkCompareCanvasSB != null)
            {
                blinkCompareCanvasSB.Stop();
                compareCanvas.Opacity = 1;
            }

            _linksList.Clear();
            if (linksCanvas != null)
            {
                foreach (LinkEllipse linkEllipse in linksCanvas.Children)
                {
                    linkEllipse.DataContext = null;
                }

                linksCanvas.Children.Clear();
            }

            // dispose highlight criteria
            if (highlightCanvas != null)
            {
                _highlightList.Clear();
                highlightCanvas.Children.Clear();
            }

            if (borderCanvas != null)
            {
                borderCanvas.Children.Clear();
            }


        }

        void linkEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void DisposeNotes()
        {
            //foreach (Note note in notesList)
            //    note.Dispose();

            foreach (DocLink link in _linksList)
                link.Dispose();

            //notesList.Clear();

            _linksList.Clear();
            if (drawingCanvas != null)
            {
                drawingCanvas.Children.Clear();
                _shapeMover = null;
                _shapeHandler1 = null;
                _shapeHandler2 = null;
                ClearCurrentNotifShapeList();
            }
        }

        private void DisposeGrid()
        {
            if (gridCanvas != null)
            {
                gridCanvas.Children.Clear();
            }
            if (rowHeaders != null)
            {
                rowHeaders.Children.Clear();
            }
            if (colHeaders != null)
            {
                colHeaders.Children.Clear();
            }

            origineXLine = null;
            origineYLine = null;
        }

        private bool progressiveZoom;
        public bool ProgressiveZoom
        {
            get { return progressiveZoom; }
            set
            {
                if (progressiveZoom == value)
                    return;
                progressiveZoom = value;
                UpdateTiles(this.ActualWidth, this.ActualHeight);
            }
        }


        private bool displayGrid;
        public bool DisplayGrid
        {
            get { return displayGrid; }
            set
            {
                if (displayGrid == value)
                    return;
                displayGrid = value;

                if (gridParent != null)
                {
                    if (displayGrid)
                        gridParent.Visibility = System.Windows.Visibility.Visible;
                    else
                        gridParent.Visibility = System.Windows.Visibility.Collapsed;
                }

                if (gridOptionsBtn != null)
                    gridOptionsBtn.IsEnabled = !editNote && isAPlanShown;
            }
        }

        private bool displayLinks;
        public bool DisplayLinks
        {
            get { return displayGrid; }
            set
            {
                if (value != displayLinks)
                {
                    displayLinks = value;
                    UpdateLinks();
                }
            }
        }

        private Guid compareWithVersionFolderID;
        public Guid CompareWithVersionFolderID
        {
            get { return compareWithVersionFolderID; }
            set
            {
                if (compareWithVersionFolderID != value)
                {
                    compareWithVersionFolderID = value;
                }
            }
        }

        private Guid compareFolderID;
        public Guid ComparFolderID
        {
            get { return compareFolderID; }
            set
            {
                if (compareFolderID != value)
                {
                    compareFolderID = value;
                }
            }
        }

        private string compareWithVersionTilePath;
        public string CompareWithVersionTilePath
        {
            get { return compareWithVersionTilePath; }
            set
            {
                if (compareWithVersionTilePath != value)
                {
                    compareWithVersionTilePath = value;
                }
            }
        }

        private int compareWithVersion = -1;
        private int compareWithPage = -1;
        public void CompareWithVersion(int version, int page)
        {
            if ((compareWithVersion != version) || (compareWithPage != page))
            {
                compareWithVersion = version;
                compareWithPage = page;
                UpdateCompareCanvas();
            }
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            if ((this.ActualHeight != finalSize.Height) || (this.ActualWidth != finalSize.Width))
            {
                UpdateTiles(finalSize.Width, finalSize.Height);
                ClipToBounds(finalSize);
            }

            return base.ArrangeOverride(finalSize);
        }

        private void ClipToBounds(Size size)
        {
            RectangleGeometry clipRect = new RectangleGeometry();
            clipRect.RadiusX = 3;
            clipRect.RadiusY = 3;
            clipRect.Rect = new Rect(0, 0, size.Width, size.Height);
            this.Clip = clipRect;
        }

        Line origineXLine;
        Line origineYLine;
        int firstHRectangleIndex = 0;
        private void InitializeGrid()
        {
            if (this.Visibility == System.Windows.Visibility.Collapsed)
                return;

            if (!isLoaded || !isTemplateApplied)
                return;

            this.gridOptionsBtn.IsEnabled = !editNote;

            firstHRectangleIndex = 0;
            int maxWidth = _pictureWidth; //layers[0].TileWidth * 256;
            int maxHeight = _pictureHeight; //layers[0].TileHeight * 256;


            Rectangle firstVRectangle = new Rectangle();
            firstVRectangle.Height = maxHeight;
            firstVRectangle.Fill = new SolidColorBrush(ColorHelper.IntToColorWithAlpha(gridOptions.ColorWithAlpha)); //Color.FromArgb(15, 0, 0, 0));
            Canvas.SetLeft(firstVRectangle, 0);
            Canvas.SetTop(firstVRectangle, 0);

            gridCanvas.Children.Add(firstVRectangle);
            firstHRectangleIndex++;

            Border firstColHeader = CreateColHeader();
            colHeaders.Children.Add(firstColHeader);

            for (int x = 0, c = 0; x < maxWidth; x += gridOptions.CellSize, c++)
            {

                if (c % 2 == 0)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Height = maxHeight;

                    rectangle.Fill = new SolidColorBrush(ColorHelper.IntToColorWithAlpha(gridOptions.ColorWithAlpha));
                    Canvas.SetTop(rectangle, 0);

                    gridCanvas.Children.Add(rectangle);
                    firstHRectangleIndex++;
                }

                Border colHeader = CreateColHeader();
                colHeaders.Children.Add(colHeader);
            }

            Rectangle firstHRectangle = new Rectangle();
            firstHRectangle.Width = maxWidth;
            firstHRectangle.Fill = new SolidColorBrush(ColorHelper.IntToColorWithAlpha(gridOptions.ColorWithAlpha));
            Canvas.SetLeft(firstHRectangle, 0);
            Canvas.SetTop(firstHRectangle, 0);

            gridCanvas.Children.Add(firstHRectangle);

            Border firstRowHeader = CreateRowHeader();
            rowHeaders.Children.Add(firstRowHeader);

            for (int y = 0, r = 0; y < maxHeight; y += gridOptions.CellSize, r++)
            {

                if (r % 2 == 0)
                {
                    Rectangle rectangle = new Rectangle();

                    rectangle.Width = maxWidth;
                    rectangle.Fill = new SolidColorBrush(ColorHelper.IntToColorWithAlpha(gridOptions.ColorWithAlpha));
                    Canvas.SetLeft(rectangle, 0);

                    gridCanvas.Children.Add(rectangle);
                }

                Border rowHeader = CreateRowHeader();
                rowHeaders.Children.Add(rowHeader);
            }


            origineXLine = new Line();
            origineXLine.Stroke = new SolidColorBrush(ColorHelper.IntToColorWithoutAlpha(gridOptions.ColorWithoutAlpha));
            origineXLine.StrokeThickness = 4;
            origineXLine.X1 = 0;
            origineXLine.X2 = 0;
            origineXLine.Y1 = 0;
            origineXLine.Y2 = maxHeight;

            origineYLine = new Line();
            origineYLine.Stroke = new SolidColorBrush(ColorHelper.IntToColorWithoutAlpha(gridOptions.ColorWithoutAlpha));
            origineYLine.StrokeThickness = 4;
            origineYLine.X1 = 0;
            origineYLine.X2 = maxWidth;
            origineYLine.Y1 = 0;
            origineYLine.Y2 = 0;


            if (isGridOptionVisible)
            {
                origineYLine.Visibility = System.Windows.Visibility.Visible;
                origineXLine.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                origineYLine.Visibility = System.Windows.Visibility.Collapsed;
                origineXLine.Visibility = System.Windows.Visibility.Collapsed;
            }

            gridCanvas.Children.Add(origineYLine);
            gridCanvas.Children.Add(origineXLine);

            if (displayGrid)
                gridParent.Visibility = System.Windows.Visibility.Visible;
            else
                gridParent.Visibility = System.Windows.Visibility.Collapsed;


        }

        private Border CreateColHeader()
        {
            TextBlock t1 = new TextBlock();
            t1.Width = gridOptions.CellSize;
            t1.FontSize = 16;
            t1.TextAlignment = TextAlignment.Center;
            t1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            t1.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            Border border = new Border();
            border.Height = 24;
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = new SolidColorBrush(Colors.Gray);

            border.Child = t1;

            return border;
        }

        private Border CreateRowHeader()
        {
            TextBlock t1 = new TextBlock();
            t1.FontSize = 16;
            t1.TextAlignment = TextAlignment.Center;
            t1.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            Border border = new Border();
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = new SolidColorBrush(Colors.Gray);
            border.Width = 30;

            border.Child = t1;

            return border;
        }

        private string GetRowAlphabeticHeader(int colNbr)
        {
            string result = "";
            int currentNbr = Math.Abs(colNbr);
            if (colNbr < 0)
                currentNbr--;

            while (currentNbr > 25)
            {
                int mod = currentNbr % 26;
                result = ((char)(65 + mod)).ToString() + result;

                currentNbr = (currentNbr - mod) / 26 - 1;

            }

            result = ((char)(65 + currentNbr)).ToString() + result;
            if (colNbr < 0)
                result = "-" + result;

            return result;
        }

        private readonly List<Tile> _tilesList = new List<Tile>();
        //Dictionary<string, string> imageCache = new Dictionary<string, string>();
        private bool _isInProcess = false;
        private readonly object _inProcessSynch = new object();

        private void UpdateTiles(double actualWidth, double actualHeight)
        {
            if (Visibility == Visibility.Collapsed)
                return;

            if (PlanId == -1)
                return;

            lock (_inProcessSynch)
            {
                if (_isInProcess)
                    return;

                if (!isLoaded || !isTemplateApplied)
                    return;

                _isInProcess = true;
            }

            int maxTilesListCount = 400;
            if (_tileSize > 128)
                maxTilesListCount = 100;

            try
            {
                Rect viewport = new Rect(0, 0, actualWidth, actualHeight);
                if (RenderLevel >= 0)
                {
                    if (RenderLevel >= _layers.Count)
                        return; //if we compare 2 documents with differents nbr of zoom

                    //int globalTileSize = 256;
                    //if (layers[0].PixelWidth / layers[0].TileWidth < 200)
                    //    globalTileSize = 128;

                    // scales are used for the progressive zoom and the display of the positionning grid over the images:
                    // - the current level is always displayed with a tile size of 256x256 (ScaleXY = 1)
                    // - the upper level (+1) is is displayed with a tile size of 512x512 (ScaleXY = 2), etc
                    // - the lower level (-1) is is displayed with a tile size of 512x512 (ScaleXY = 0.5), etc
                    int nbrLevels = _layers.Count;
                    int maxLevel = nbrLevels - 1;
                    scales = new double[nbrLevels];
                    scales[RenderLevel] = 1.0;
                    for (int i = RenderLevel - 1; i >= 0; i--)
                    {
                        scales[i] = scales[i + 1] / 2.0;
                    }
                    for (int i = RenderLevel + 1; i <= maxLevel; i++)
                    {
                        scales[i] = scales[i - 1] * 2.0;
                    }
                    RotateTransform tileRotateTransform = null;
                    switch (_rotateAngle)
                    {
                        case 90:
                            tileRotateTransform = new RotateTransform();
                            tileRotateTransform.CenterX = _tileSize / 2;
                            tileRotateTransform.CenterY = _tileSize / 2;
                            tileRotateTransform.Angle = 90;
                            break;

                        case 180:
                            tileRotateTransform = new RotateTransform();
                            tileRotateTransform.CenterX = _tileSize / 2;
                            tileRotateTransform.CenterY = _tileSize / 2;
                            tileRotateTransform.Angle = 180;
                            break;

                        case 270:
                            tileRotateTransform = new RotateTransform();
                            tileRotateTransform.CenterX = _tileSize / 2;
                            tileRotateTransform.CenterY = _tileSize / 2;
                            tileRotateTransform.Angle = 270;
                            break;

                    }



                    // render layer size
                    rlPixelWidth = _layers[RenderLevel].PixelWidth;
                    rlPixelHeight = _layers[RenderLevel].PixelHeight;
                    int zindex = 0;
                    // process each layer, from bottom (layer 0 = full resolution) to top (layer n = thumbnail of max 256x256)
                    for (int level = maxLevel; level >= 0; level--)
                    {
                        Layer l = _layers[level];
                        // should we show or hide this layer ?
                        // - when we are not using progressive zoom, we only display the current layer
                        // - when we are using progressive zoom, we display all the layers, except when we are at top level (only 1 tile)
                        //   in which case we hide the lower levels to avoid artifact due to very small scaling of lower level tiles
                        // - when we are using progressive zoom and level < renderLevel, there is no need to display the images whith a higher resolution


                        Transform bigThumbTransform = null;
                        if (l.IsBigThumb)
                        {
                            switch (_rotateAngle)
                            {
                                case 90:
                                    RotateTransform bigThumbRotate90Transform = new RotateTransform();
                                    bigThumbRotate90Transform.CenterX = 0;
                                    bigThumbRotate90Transform.CenterY = 0;
                                    bigThumbRotate90Transform.Angle = 90;

                                    TranslateTransform bigThumbTranslate90Transform = new TranslateTransform();
                                    bigThumbTranslate90Transform.X = l.PixelWidth;
                                    bigThumbTranslate90Transform.Y = 0;

                                    bigThumbTransform = new TransformGroup();
                                    ((TransformGroup)bigThumbTransform).Children.Add(bigThumbRotate90Transform);
                                    ((TransformGroup)bigThumbTransform).Children.Add(bigThumbTranslate90Transform);
                                    break;

                                case 180:
                                    RotateTransform bigThumbRotate180Transform = new RotateTransform();
                                    bigThumbRotate180Transform.CenterX = l.PixelWidth / 2;
                                    bigThumbRotate180Transform.CenterY = l.PixelHeight / 2;
                                    bigThumbRotate180Transform.Angle = 180;

                                    bigThumbTransform = bigThumbRotate180Transform;
                                    break;

                                case 270:
                                    RotateTransform bigThumbRotate270Transform = new RotateTransform();
                                    bigThumbRotate270Transform.CenterX = 0;
                                    bigThumbRotate270Transform.CenterY = 0;
                                    bigThumbRotate270Transform.Angle = 270;

                                    TranslateTransform bigThumbTranslate270Transform = new TranslateTransform();
                                    bigThumbTranslate270Transform.X = 0;
                                    bigThumbTranslate270Transform.Y = l.PixelHeight;

                                    bigThumbTransform = new TransformGroup();
                                    ((TransformGroup)bigThumbTransform).Children.Add(bigThumbRotate270Transform);
                                    ((TransformGroup)bigThumbTransform).Children.Add(bigThumbTranslate270Transform);
                                    break;

                            }
                        }

                        bool hideAllTiles = (!progressiveZoom && level != RenderLevel) ||
                                            (progressiveZoom && RenderLevel == maxLevel && level < maxLevel) ||
                                            (progressiveZoom && level < RenderLevel);

                        if (hideAllTiles)
                        {
                            for (int i = 0; i < l.Tiles.Length; i++)
                            {
                                zindex++;
                                Tile tile = l.Tiles[i];
                                if (tile != null)
                                {
                                    HideTile(tile);

                                }
                            }

                        }
                        else
                        {
                            double scale = scales[level];
                            int relativeTileSize = (int)(_tileSize * scale);
                            double deltaLeft = 0;
                            double deltaTop = 0;
                            if (!l.IsBigThumb)
                            {
                                switch (_rotateAngle)
                                {
                                    case 90:
                                        deltaLeft = (l.TileWidth * relativeTileSize) - rlPixelWidth;
                                        //deltaTop = (l.TileHeight * relativeTileSize) - rlPixelHeight;
                                        break;

                                    case 180:
                                        deltaLeft = (l.TileWidth * relativeTileSize) - rlPixelWidth;
                                        deltaTop = (l.TileHeight * relativeTileSize) - rlPixelHeight;
                                        break;

                                    case 270:
                                        //deltaLeft = (l.TileWidth * relativeTileSize) - rlPixelWidth;
                                        deltaTop = (l.TileHeight * relativeTileSize) - rlPixelHeight;
                                        break;
                                }
                            }

                            //v.SetStroke(1, Color.Red);
                            for (int x = 0; x < l.TileWidth; x++)
                            {

                                int left = (int)(x * relativeTileSize - (rlPixelWidth / 2.0) + _renderCenter.X - deltaLeft);

                                for (int y = 0; y < l.TileHeight; y++)
                                {
                                    zindex++;

                                    int top = (int)(y * relativeTileSize - (rlPixelHeight / 2.0) + _renderCenter.Y - deltaTop);

                                    Tile tile = l.Tiles[x + y * l.TileWidth];

                                    Rect r = new Rect(left, top, relativeTileSize, relativeTileSize);
                                    if (l.IsBigThumb)
                                    {
                                        r = new Rect(left, top, rlPixelWidth, rlPixelHeight);
                                    }
                                    //if (viewport.Contains(new Point(r.Left, r.Top)) || viewport.Contains(new Point(r.Bottom, r.Right)) ||
                                    //    viewport.Contains(new Point(r.Left, r.Bottom)) || viewport.Contains(new Point(r.Top, r.Right)) ||
                                    //    r.Contains(new Point(viewport.Left, viewport.Top)) || r.Contains(new Point(viewport.Bottom, viewport.Right)) ||
                                    //    r.Contains(new Point(viewport.Left, viewport.Bottom)) || r.Contains(new Point(viewport.Top, viewport.Right)))
                                    Rect viewportInterect = new Rect(viewport.Left, viewport.Top, viewport.Width, viewport.Height);
                                    viewportInterect.Intersect(r);
                                    if (viewportInterect != Rect.Empty)
                                    {
                                        if ((level == RenderLevel) || (level == (RenderLevel + 1)) || (level == (RenderLevel + 2)) || (level == maxLevel) || (level == (maxLevel - 1)))
                                        {
                                            if (tile != null)
                                            {
                                                Canvas.SetLeft(tile.Image, left);
                                                Canvas.SetTop(tile.Image, top);
                                                if (scale == 1.0)
                                                {
                                                    if (l.IsBigThumb)
                                                        tile.Image.RenderTransform = bigThumbTransform;
                                                    else
                                                        tile.Image.RenderTransform = tileRotateTransform;
                                                }
                                                else
                                                {
                                                    ScaleTransform scaleTransform = new ScaleTransform();
                                                    scaleTransform.ScaleX = scale;
                                                    scaleTransform.ScaleY = scale;
                                                    scaleTransform.CenterX = 0;
                                                    scaleTransform.CenterY = 0;

                                                    if (l.IsBigThumb && bigThumbTransform != null)
                                                    {
                                                        TransformGroup fullThumbTransform = new TransformGroup();
                                                        fullThumbTransform.Children.Add(bigThumbTransform);
                                                        fullThumbTransform.Children.Add(scaleTransform);

                                                        tile.Image.RenderTransform = fullThumbTransform;
                                                    }
                                                    else if (tileRotateTransform != null)
                                                    {
                                                        TransformGroup fullTileTransform = new TransformGroup();
                                                        fullTileTransform.Children.Add(tileRotateTransform);
                                                        fullTileTransform.Children.Add(scaleTransform);

                                                        tile.Image.RenderTransform = fullTileTransform;
                                                    }
                                                    else
                                                    {
                                                        tile.Image.RenderTransform = scaleTransform;
                                                    }
                                                }


                                                tile.Image.Visibility = Visibility.Visible;
                                                Canvas.SetZIndex(tile.Image, zindex);
                                            }
                                            else
                                            {

                                                if (_tilesList.Count < maxTilesListCount) //TODO calculate max tilesList Count dynamically according to the tile size and the screen size
                                                {
                                                    int tileIndex = x + y * l.TileWidth;
                                                    tile = new Tile(new Image(), level, tileIndex);

                                                    tile.Image.Stretch = Stretch.None;
                                                    WeakEventHelper.RegisterImageFailed(tile.Image, this, (ls, s, e) => ls.img_ImageFailed(s, e));
                                                    WeakEventHelper.RegisterImageOpened(tile.Image, this, (ls, s, e) => ls.Image_ImageOpened(s, e));
                                                    imagesCanvas.Children.Add(tile.Image);

                                                    l.Tiles[tileIndex] = tile;
                                                    _tilesList.Add(tile);
                                                }
                                                else
                                                {
                                                    tile = _tilesList[0];
                                                    //Remove the tile from the tilelayer it belongs to
                                                    Layer tileLayer = _layers[tile.LayerLevel];
                                                    tileLayer.Tiles[tile.ImageIndex] = null;

                                                    //Add the tile to the current tilelayer
                                                    int tileIndex = x + y * l.TileWidth;
                                                    l.Tiles[tileIndex] = tile;
                                                    tile.LayerLevel = level;
                                                    tile.ImageIndex = tileIndex;

                                                    //tile.Image.Source = null; //BUG FireFox
                                                    imagesCanvas.Children.Remove(tile.Image);

                                                    tile.Image = new Image();
                                                    tile.Image.Stretch = Stretch.None;

                                                    WeakEventHelper.RegisterImageFailed<PictureViewer>(tile.Image, this, (ls, s, e) => ls.img_ImageFailed(s, e));
                                                    WeakEventHelper.RegisterImageOpened<PictureViewer>(tile.Image, this, (ls, s, e) => ls.Image_ImageOpened(s, e));
                                                    imagesCanvas.Children.Add(tile.Image);

                                                    _tilesList.Remove(tile);
                                                    _tilesList.Add(tile);

                                                    tile.Image.Visibility = Visibility.Visible;
                                                }
                                                Canvas.SetLeft(tile.Image, r.X);
                                                Canvas.SetTop(tile.Image, r.Y);
                                                Canvas.SetZIndex(tile.Image, zindex);

                                                if (l.IsBigThumb)
                                                {
                                                    //In the case the user click to Tutorial button from login page, we don't have the current user
                                                    string alias = Global.Instance.CurrentUser != null ? Global.Instance.CurrentUser.Alias : "";
                                                    string thumbUrl = Global.Instance.RootUrl + _tilesPath + "/bigthumb.jpg?device=web&k=" + alias;
                                                    //Debug.WriteLine("Loading " + tileName);
                                                    tile.Image.Name = this.Name + ":" + thumbUrl;
                                                    tile.Image.RenderTransform = bigThumbTransform;
                                                    if (PlanId != 0) //blank plan
                                                    {
                                                        tile.Image.Source = new BitmapImage(new Uri(thumbUrl, UriKind.Absolute));
                                                    }
                                                }
                                                else
                                                {
                                                    int rotatex = x;
                                                    int rotatey = y;
                                                    switch (_rotateAngle)
                                                    {
                                                        case 90:
                                                            rotatex = y;
                                                            rotatey = l.TileWidth - 1 - x;
                                                            break;

                                                        case 180:
                                                            rotatex = l.TileWidth - 1 - x;
                                                            rotatey = l.TileHeight - 1 - y;
                                                            break;

                                                        case 270:
                                                            rotatex = l.TileHeight - 1 - y;
                                                            rotatey = x;
                                                            break;

                                                    }

                                                    //In the case the user click to Tutorial button from login page, we don't have the current user
                                                    string alias = Global.Instance.CurrentUser != null ? Global.Instance.CurrentUser.Alias : "";
                                                    string tileUrl = Global.Instance.RootUrl + _tilesPath + "/" + l.Level + "-" + rotatex + "-" + rotatey + ".jpg?device=web&k=" + alias;
                                                    //Debug.WriteLine("Loading " + tileName);
                                                    tile.Image.Name = this.Name + ":" + tileUrl;
                                                    tile.Image.RenderTransform = tileRotateTransform;
                                                    if (PlanId != 0) //blank plan
                                                    {
                                                        tile.Image.Source = new BitmapImage(new Uri(tileUrl, UriKind.Absolute));
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (tile != null)
                                            {
                                                HideTile(tile);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (tile != null)
                                        {
                                            HideTile(tile);
                                        }
                                    }
                                }
                            }
                        }


                    }

                    this.ViewPort = new Rect(0.5 - _renderCenter.X / rlPixelWidth, 0.5 - _renderCenter.Y / rlPixelHeight, actualWidth / rlPixelWidth, actualHeight / rlPixelHeight);
                    CurrentScale = GetCurrentScale();
                    UpdateGrid(scales[0], rlPixelWidth, rlPixelHeight);
                    UpdateDrawingCanvas(scales[0], rlPixelWidth, rlPixelHeight);
                    UpdateCompareCanvas(rlPixelWidth, rlPixelHeight);
                    ShowCurrentShapeHandlers();
                    ShowCurrentShapeMover();
                    UpdateLinks();
                    UpdateSelectedArea(scales[0], rlPixelWidth, rlPixelHeight);
                    UpdatePin(scales[0], rlPixelWidth, rlPixelHeight);
                    UpdateBorderCanvas();
                    UpdateHighlight();
                }

            }
            finally
            {
                _isInProcess = false;
            }
        }

        /// <summary>
        /// To show first highlight or update current highlight
        /// </summary>
        void UpdateHighlight()
        {
            if (_IsShowhighlightFirst)
            {
                _IsShowhighlightFirst = false;
                GotoHighLight(0);
            }
            else
            {
                DrawHighlight(_highlightIndex);
            }
        }

        void Image_ImageOpened(object sender, RoutedEventArgs e)
        {
            if (!_imageLoadedComplete)
            {
                if (LoadImageCompleted != null)
                    LoadImageCompleted(this, EventArgs.Empty);
                _imageLoadedComplete = true;
            }
        }


        #region FT12227 Zoom by progress bar
        public double CurrentScale
        {
            get
            {
                return _currentScale;
            }
            set
            {
                if (_currentScale != value)
                {
                    _currentScale = value;
                    if (CurrentScaleChanged != null)
                        CurrentScaleChanged(this, EventArgs.Empty);
                }
            }
        }
        #endregion



        private void UpdateHighlight(double scale, int rlPixelWidth, int rlPixelHeight)
        {
            if (_highlightList.Count == 0) return;

            var imageTop = Math.Floor(_renderCenter.Y - (rlPixelHeight / 2.0));
            var imageLeft = Math.Floor(_renderCenter.X - (rlPixelWidth / 2.0));

            Duration duration = new Duration(TimeSpan.FromSeconds(0.5));
            higlightBoard.Stop();

            foreach (var item in _highlightList)
            {
                if (item.Rectangle == null)
                {
                    item.Rectangle = new Rectangle()
                    {
                        Width = item.OriginalRect.Width,
                        Height = item.OriginalRect.Height,
                        Fill = new SolidColorBrush(Color.FromArgb(127, 255, 0, 0)),
                    };

                    highlightCanvas.Children.Add(item.Rectangle);

                    DoubleAnimation blink = new DoubleAnimation();
                    blink.Duration = duration;
                    blink.From = 1;
                    blink.To = 0;
                    blink.RepeatBehavior = RepeatBehavior.Forever;
                    blink.AutoReverse = true;
                    higlightBoard.Children.Add(blink);

                    Storyboard.SetTarget(blink, item.Rectangle);
                    Storyboard.SetTargetProperty(blink, new PropertyPath("Opacity"));
                }

                Canvas.SetTop(item.Rectangle, imageTop + item.OriginalRect.Y * scale);
                Canvas.SetLeft(item.Rectangle, imageLeft + item.OriginalRect.X * scale);
                item.Rectangle.RenderTransform = (scale == 1.0) ? null : new ScaleTransform() { ScaleX = scale, ScaleY = scale, CenterX = 0, CenterY = 0 };
            }

            higlightBoard.Begin();
        }

        public void DrawHighlight(int highlightIndex)
        {
            if (_highlightList.Count == 0) return;
            _highlightIndex = highlightIndex;

            var imgW = _layers[this.RenderLevel].PixelWidth;
            var imgH = _layers[this.RenderLevel].PixelHeight;

            var imageTop = Math.Floor(_renderCenter.Y - (imgH / 2.0));
            var imageLeft = Math.Floor(_renderCenter.X - (imgW / 2.0));

            Duration duration = new Duration(TimeSpan.FromSeconds(0.5));
            higlightBoard.Stop();
            highlightCanvas.Children.Clear();
            {
                var item = _highlightList[highlightIndex];
                if (item.Rectangle == null)
                {
                    item.Rectangle = new Rectangle()
                    {
                        Fill = new SolidColorBrush(Color.FromArgb(127, 255, 0, 0)),
                    };
                    DoubleAnimation blink = new DoubleAnimation();
                    blink.Duration = duration;
                    blink.From = 1;
                    blink.To = 0;
                    blink.RepeatBehavior = RepeatBehavior.Forever;
                    blink.AutoReverse = true;
                    higlightBoard.Children.Add(blink);

                    Storyboard.SetTarget(blink, item.Rectangle);
                    Storyboard.SetTargetProperty(blink, new PropertyPath("Opacity"));
                }
                highlightCanvas.Children.Add(item.Rectangle);
                item.Rectangle.Width = item.OriginalRect.Width;
                item.Rectangle.Height = item.OriginalRect.Height;
                var scale = GetCurrentScale();
                var leftItem = imageLeft + item.OriginalRect.X * scale;
                var topItem = imageTop + item.OriginalRect.Y * scale;
                Canvas.SetTop(item.Rectangle, topItem);
                Canvas.SetLeft(item.Rectangle, leftItem);
                var scaleTransform = (GetCurrentScale() == 1.0) ? null : new ScaleTransform() { ScaleX = scale, ScaleY = scale, CenterX = 0, CenterY = 0 };

                //Re-caculate size after had been scaled. Minimum size for highlight (Min-width = 16, Min-height = 10)
                var actualH = item.Rectangle.ActualHeight * scale;
                if (actualH < 10)
                {
                    item.Rectangle.Height = 10 / scale;
                    if (scaleTransform != null)
                        scaleTransform.CenterY = -3 ;
                }
                var actualW = item.Rectangle.ActualWidth * GetCurrentScale();
                if (actualW < 16)
                {
                    item.Rectangle.Width = 16 / scale;
                    if (scaleTransform != null)
                        scaleTransform.CenterX = -8 ;
                }
                item.Rectangle.RenderTransform = scaleTransform;
            }
            higlightBoard.Begin();
        }


        /// <summary>
        /// Move view port to hightlight index
        /// </summary>
        /// <param name="hightLightIndex"></param>
        public void GotoHighLight(int hightLightIndex)
        {

            DrawHighlight(hightLightIndex);
            if (highlightCanvas.Children == null || highlightCanvas.Children.Count == 0)
                return;
            var itemHighLight = highlightCanvas.Children[0];
            var topItem = Canvas.GetTop(itemHighLight);
            var leftItem = Canvas.GetLeft(itemHighLight);
            var imgW = _layers[this.RenderLevel].PixelWidth;
            var imgH = _layers[this.RenderLevel].PixelHeight;
            var ycenter = _renderCenter.Y + this.ActualHeight / 2 - topItem;
            var xcenter = _renderCenter.X + this.ActualWidth / 2 - leftItem;

            if (leftItem > 0 && leftItem <= this.ActualWidth)
                xcenter = _renderCenter.X;
            else if (leftItem > this.ActualWidth)
                xcenter = _renderCenter.X - (leftItem - this.ActualWidth / 2);

            if (topItem > 0 && topItem <= this.ActualHeight)
                ycenter = _renderCenter.Y;
            else if (topItem > this.ActualHeight)
                ycenter = _renderCenter.Y - (topItem - this.ActualHeight / 2);
            _renderCenter.X = xcenter;
            _renderCenter.Y = ycenter;
            UpdateTiles(this.ActualWidth, this.ActualHeight);
        }

        private void UpdatePin(double scale, int rlPixelWidth, int rlPixelHeight)
        {
            if (_canShowPin)
            {
                _pinImage.Visibility = System.Windows.Visibility.Visible;
                var imageTop = Math.Floor(_renderCenter.Y - (rlPixelHeight / 2.0));
                var imageLeft = Math.Floor(_renderCenter.X - (rlPixelWidth / 2.0));

                Canvas.SetTop(_pinImage, imageTop + (_pinOriginPoint.Y - 128) * scale);
                Canvas.SetLeft(_pinImage, imageLeft + _pinOriginPoint.X * scale);
                _pinImage.RenderTransform = (scale == 1.0) ? null : new ScaleTransform() { ScaleX = scale, ScaleY = scale, CenterX = 0, CenterY = 0 };
            }
        }

        private void UpdateLinks()
        {
            if (linksCanvas == null)
                return;

            foreach (LinkEllipse linkEllipse in linksCanvas.Children)
            {
                linkEllipse.DataContext = null;
            }

            linksCanvas.Children.Clear();

            if (!displayLinks)
                return;

            int rlPixelWidth = _layers[RenderLevel].PixelWidth;
            int rlPixelHeight = _layers[RenderLevel].PixelHeight;

            double viewPortLeft = rlPixelWidth / 2 - _renderCenter.X;
            double viewPortTop = rlPixelHeight / 2 - _renderCenter.Y;

            double viewPortRight = viewPortLeft + this.ActualWidth;
            double viewPortBottom = viewPortTop + this.ActualHeight;


            foreach (DocLink link in LinksList)
            {
                double linkLeft = rlPixelWidth * link.Left;
                double linkTop = rlPixelHeight * link.Top;

                if ((linkLeft >= viewPortLeft) && (link.Left <= viewPortRight) &&
                    (linkTop >= viewPortTop) && (linkTop <= viewPortBottom))
                {
                    LinkEllipse linkEllipse = new LinkEllipse();
                    linkEllipse.DataContext = link;
                    linkEllipse.Width = 16;
                    linkEllipse.Height = 16;
                    WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(linkEllipse, this, (ls, s, e) => ls.linkEllipse_MouseLeftButtonUp(s, e));
                    WeakEventHelper.RegisterMouseLeftButtonUp<PictureViewer>(linkEllipse, this, (ls, s, e) => ls.linkEllipse_MouseLeftButtonDown(s, e));

                    linkEllipse.Cursor = Cursors.Arrow;

                    Canvas.SetLeft(linkEllipse, linkLeft - viewPortLeft - 16);
                    Canvas.SetTop(linkEllipse, linkTop - viewPortTop - 16);

                    linksCanvas.Children.Add(linkEllipse);
                }
            }

        }

        public event EventHandler<PlanEventArgs> OpenLink;
        void linkEllipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OpenLink != null)
            {
                LinkEllipse linkEllipse = (LinkEllipse)sender;
                if (linkEllipse != null)
                {
                    DocLink docLink = (DocLink)linkEllipse.DataContext;
                    if (docLink != null)
                    {
                        OpenLink(this, new PlanEventArgs(docLink.PlanGuidID));
                    }
                }
            }

        }

        void img_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            //Image image = (Image)sender;
            //ICIMessageBox.ShowError(Global.Instance.LanguageDictionary["PictureViewer_GetTilesErrorMessage"] + "\r\n(" + image.Name + ")", Global.Instance.LanguageDictionary["PictureViewer_GetTilesErrorTitle"], e.ErrorException);
            ErrorLoadingTiles = true;

        }

        private bool errorLoadingTiles;
        private bool ErrorLoadingTiles
        {
            get { return errorLoadingTiles; }
            set
            {
                if (errorLoadingTiles != value)
                {
                    errorLoadingTiles = value;
                    UpdateErrorLoadingTiles();
                }
            }
        }

        private void UpdateErrorLoadingTiles()
        {
            if (errorLoadingTilesGrid != null)
            {
                if (errorLoadingTiles)
                {
                    errorLoadingTilesGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    errorLoadingTilesGrid.Visibility = Visibility.Collapsed;
                }

            }
        }

        private void HideTile(Tile tile)
        {
            tile.Image.Visibility = Visibility.Collapsed;
            _tilesList.Remove(tile);
            _tilesList.Insert(0, tile);

            //Layer tileLayer = layers[tile.LayerLevel];
            //tileLayer.Tiles[tile.ImageIndex] = null;
            //tile.TileName = "";

        }

        void UpdateGrid()
        {
            if (Visibility == Visibility.Collapsed)
                return;

            if (!isAPlanShown)
                return;
            if (_layers.Count - 1 < RenderLevel)
                return;

            Layer l = _layers[RenderLevel];
            UpdateGrid(GetCurrentScale(), l.PixelWidth, l.PixelHeight);
        }

        void UpdateDrawingCanvas()
        {
            if (Visibility == Visibility.Collapsed)
                return;

            if (!isAPlanShown)
                return;
            if (_layers.Count - 1 < RenderLevel)
                return;

            Layer l = _layers[RenderLevel];
            UpdateDrawingCanvas(GetCurrentScale(), l.PixelWidth, l.PixelHeight);
        }

        private static double GetScale(int renderLevel)
        {
            double scale = 1.0;
            for (int i = renderLevel - 1; i >= 0; i--)
            {
                scale = scale / 2.0;
            }

            return scale;
        }

        private double GetCurrentScale()
        {
            return GetScale(RenderLevel);
        }

        private int GetRenderLevel(double scale)
        {
            bool isScaleToSmall = false;
            return GetRenderLevel(scale, out isScaleToSmall);
        }

        private int GetRenderLevel(double scale, out bool isScaleToSmall)
        {
            isScaleToSmall = false;
            int currentRenderLevel = 0;
            double currentScale = 1.0;
            while (!DoubleAproximativelyEquals(scale, currentScale))
            {

                currentScale = currentScale / 2.0;
                currentRenderLevel++;
            }

            if (currentRenderLevel >= _layers.Count)
            {
                isScaleToSmall = true;
                currentRenderLevel = _layers.Count - 1;
            }

            if (currentRenderLevel < 0)
                currentRenderLevel = 0;

            return currentRenderLevel;
        }

        private static bool DoubleAproximativelyEquals(double d1, double d2)
        {
            return (d1 + 0.00001 > d2) && (d1 - 0.00001 < d2);

        }

        void UpdateCompareCanvas()
        {
            if (Visibility == Visibility.Collapsed)
                return;

            if (!isAPlanShown)
                return;
            if (_layers.Count - 1 < RenderLevel)
                return;

            Layer l = _layers[RenderLevel];
            UpdateCompareCanvas(l.PixelWidth, l.PixelHeight);
        }

        void UpdateCompareCanvas(int rlPixelWidth, int rlPixelHeight)
        {
            if (Visibility == Visibility.Collapsed)
                return;

            if ((compareCanvas != null) && (compareImage != null))
            {

                //Global.Instance.RootUrl + tilesPath;
                if ((compareWithVersion > 0) && (compareWithVersion != VersionNbr))
                {
                    Guid compareFolderGuid1 = FolderGuid;
                    Guid compareFolderGuid2 = CompareWithVersionFolderID;
                    string compareTilesPath = _tilesPath;
                    if (compareWithVersion > VersionNbr)
                    {
                        compareFolderGuid1 = CompareWithVersionFolderID;
                        compareFolderGuid2 = FolderGuid;
                        compareTilesPath = CompareWithVersionTilePath;
                    }

                    if (compareCanvas.Visibility != Visibility.Visible)
                    {
                        compareCanvas.Visibility = Visibility.Visible;

                        if (blinkCompareCanvasSB != null)
                            blinkCompareCanvasSB.Begin();
                    }

                    string compareUrl = Global.Instance.RootUrl + compareTilesPath + "/vcompare-" + compareFolderGuid1 + "-" + compareFolderGuid2 + ".png?device=web&k=" + Global.Instance.CurrentUser.Alias; ;
                    //Debug.WriteLine("Loading " + tileName);
                    compareImage.Name = this.Name + ":" + compareUrl;
                    compareImage.Source = new BitmapImage(new Uri(compareUrl, UriKind.Absolute));


                    Transform compareTransform = null;
                    switch (_rotateAngle)
                    {
                        case 0:
                            compareImage.Width = rlPixelWidth;
                            compareImage.Height = rlPixelHeight;
                            break;

                        case 90:
                            compareImage.Width = rlPixelHeight;
                            compareImage.Height = rlPixelWidth;

                            RotateTransform compareRotate90Transform = new RotateTransform();
                            compareRotate90Transform.CenterX = 0;
                            compareRotate90Transform.CenterY = 0;
                            compareRotate90Transform.Angle = 90;

                            TranslateTransform compareTranslate90Transform = new TranslateTransform();
                            compareTranslate90Transform.X = rlPixelWidth;
                            compareTranslate90Transform.Y = 0;

                            compareTransform = new TransformGroup();
                            ((TransformGroup)compareTransform).Children.Add(compareRotate90Transform);
                            ((TransformGroup)compareTransform).Children.Add(compareTranslate90Transform);
                            break;

                        case 180:
                            compareImage.Width = rlPixelWidth;
                            compareImage.Height = rlPixelHeight;

                            RotateTransform compareRotate180Transform = new RotateTransform();
                            compareRotate180Transform.CenterX = rlPixelWidth / 2;
                            compareRotate180Transform.CenterY = rlPixelHeight / 2;
                            compareRotate180Transform.Angle = 180;

                            compareTransform = compareRotate180Transform;
                            break;

                        case 270:
                            compareImage.Width = rlPixelHeight;
                            compareImage.Height = rlPixelWidth;

                            RotateTransform CompareRotate270Transform = new RotateTransform();
                            CompareRotate270Transform.CenterX = 0;
                            CompareRotate270Transform.CenterY = 0;
                            CompareRotate270Transform.Angle = 270;

                            TranslateTransform compareTranslate270Transform = new TranslateTransform();
                            compareTranslate270Transform.X = 0;
                            compareTranslate270Transform.Y = rlPixelHeight;

                            compareTransform = new TransformGroup();
                            ((TransformGroup)compareTransform).Children.Add(CompareRotate270Transform);
                            ((TransformGroup)compareTransform).Children.Add(compareTranslate270Transform);
                            break;

                    }

                    compareImage.RenderTransform = compareTransform;

                    Canvas.SetLeft(compareImage, _renderCenter.X - rlPixelWidth / 2.0);
                    Canvas.SetTop(compareImage, _renderCenter.Y - rlPixelHeight / 2.0);
                }
                else
                {
                    compareCanvas.Visibility = System.Windows.Visibility.Collapsed;
                    if (blinkCompareCanvasSB != null)
                    {
                        blinkCompareCanvasSB.Stop();
                        compareCanvas.Opacity = 1;
                    }
                }

            }
        }

        void UpdateDrawingCanvas(double renderScale, int rlPixelWidth, int rlPixelHeight)
        {
            if (Visibility == Visibility.Collapsed)
                return;

            if (drawingCanvas != null)
            {
                int notificationPageIndex = 0;
                if (CurrentDrawing != null)
                {
                    notificationPageIndex = CurrentDrawing.PageIndex;
                }

                if (displayNote && (_pageIndex == notificationPageIndex))
                    drawingCanvas.Visibility = Visibility.Visible;
                else
                    drawingCanvas.Visibility = Visibility.Collapsed;

                Transform fullRotateTransform = null;
                switch (_rotateAngle)
                {
                    case 90:
                        RotateTransform drawingCanvasRotate90Transform = new RotateTransform();
                        drawingCanvasRotate90Transform.CenterX = 0;
                        drawingCanvasRotate90Transform.CenterY = 0;
                        drawingCanvasRotate90Transform.Angle = 90;

                        TranslateTransform drawingCanvasTranslate90Transform = new TranslateTransform();
                        drawingCanvasTranslate90Transform.X = rlPixelWidth;
                        drawingCanvasTranslate90Transform.Y = 0;

                        fullRotateTransform = new TransformGroup();
                        ((TransformGroup)fullRotateTransform).Children.Add(drawingCanvasRotate90Transform);
                        ((TransformGroup)fullRotateTransform).Children.Add(drawingCanvasTranslate90Transform);
                        break;

                    case 180:
                        RotateTransform drawingCanvasRotate180Transform = new RotateTransform();
                        drawingCanvasRotate180Transform.CenterX = rlPixelWidth / 2;
                        drawingCanvasRotate180Transform.CenterY = rlPixelHeight / 2;
                        drawingCanvasRotate180Transform.Angle = 180;

                        fullRotateTransform = drawingCanvasRotate180Transform;
                        break;

                    case 270:
                        RotateTransform drawingCanvasRotate270Transform = new RotateTransform();
                        drawingCanvasRotate270Transform.CenterX = 0;
                        drawingCanvasRotate270Transform.CenterY = 0;
                        drawingCanvasRotate270Transform.Angle = 270;

                        TranslateTransform drawingCanvasTranslate270Transform = new TranslateTransform();
                        drawingCanvasTranslate270Transform.X = 0;
                        drawingCanvasTranslate270Transform.Y = rlPixelHeight;

                        fullRotateTransform = new TransformGroup();
                        ((TransformGroup)fullRotateTransform).Children.Add(drawingCanvasRotate270Transform);
                        ((TransformGroup)fullRotateTransform).Children.Add(drawingCanvasTranslate270Transform);
                        break;

                }

                ScaleTransform scaleTransform = null;
                double scale = renderScale;
                if (scale == 1.0)
                {
                    scaleTransform = null;
                }
                else
                {
                    scaleTransform = new ScaleTransform();
                    scaleTransform.ScaleX = scale;
                    scaleTransform.ScaleY = scale;
                    scaleTransform.CenterX = 0;
                    scaleTransform.CenterY = 0;

                }

                if (fullRotateTransform == null)
                {
                    drawingCanvas.RenderTransform = scaleTransform;
                    borderCanvas.RenderTransform = scaleTransform;
                }
                else
                {
                    if (scaleTransform == null)
                    {
                        drawingCanvas.RenderTransform = fullRotateTransform;
                        borderCanvas.RenderTransform = fullRotateTransform;
                    }
                    else
                    {
                        TransformGroup scaleAndRotateTransform = new TransformGroup();
                        scaleAndRotateTransform.Children.Add(scaleTransform);
                        scaleAndRotateTransform.Children.Add(fullRotateTransform);

                        drawingCanvas.RenderTransform = scaleAndRotateTransform;
                        borderCanvas.RenderTransform = scaleAndRotateTransform;
                    }
                }



                Canvas.SetLeft(drawingCanvas, _renderCenter.X - rlPixelWidth / 2.0);
                Canvas.SetTop(drawingCanvas, _renderCenter.Y - rlPixelHeight / 2.0);

                Canvas.SetLeft(borderCanvas, _renderCenter.X - rlPixelWidth / 2.0);
                Canvas.SetTop(borderCanvas, _renderCenter.Y - rlPixelHeight / 2.0);

            }
        }

        void UpdateGrid(double renderScale, int rlPixelWidth, int rlPixelHeight)
        {
            if (this.Visibility == System.Windows.Visibility.Collapsed)
                return;

            if ((gridCanvas == null) || (gridCanvas.Children.Count == 0))
                return;

            if ((rlPixelWidth == 0) || (rlPixelHeight == 0))
                return;

            int gridOptionsOriginX = GetGridOptionOriginX();
            int gridOptionsOriginY = GetGridOptionOriginY();

            origineXLine.X1 = gridOptionsOriginX;
            origineXLine.X2 = gridOptionsOriginX;

            origineYLine.Y1 = gridOptionsOriginY;
            origineYLine.Y2 = gridOptionsOriginY;

            int fullDeltaX = 0;
            int fullDeltaY = 0;
            int firstColIndex = 0;
            int firstRowIndex = 0;
            switch (_rotateAngle)
            {
                case 0:
                    firstColIndex = -(int)Math.Ceiling((double)gridOptionsOriginX / (double)gridOptions.CellSize);
                    firstRowIndex = -(int)Math.Ceiling((double)gridOptionsOriginY / (double)gridOptions.CellSize);
                    fullDeltaX = gridOptionsOriginX % gridOptions.CellSize;
                    fullDeltaY = gridOptionsOriginY % gridOptions.CellSize;
                    break;

                case 90:
                    firstColIndex = -(int)Math.Ceiling((double)(_pictureWidth - gridOptionsOriginX) / (double)gridOptions.CellSize);
                    firstRowIndex = -(int)Math.Ceiling((double)gridOptionsOriginY / (double)gridOptions.CellSize);
                    fullDeltaX = (_pictureWidth - gridOptionsOriginX) % gridOptions.CellSize;
                    fullDeltaY = gridOptionsOriginY % gridOptions.CellSize;
                    break;

                case 180:
                    firstColIndex = -(int)Math.Ceiling((double)(_pictureWidth - gridOptionsOriginX) / (double)gridOptions.CellSize);
                    firstRowIndex = -(int)Math.Ceiling((double)(_pictureHeight - gridOptionsOriginY) / (double)gridOptions.CellSize);
                    fullDeltaX = (_pictureWidth - gridOptionsOriginX) % gridOptions.CellSize;
                    fullDeltaY = (_pictureHeight - gridOptionsOriginY) % gridOptions.CellSize;
                    break;

                case 270:
                    firstColIndex = -(int)Math.Ceiling((double)gridOptionsOriginX / (double)gridOptions.CellSize);
                    firstRowIndex = -(int)Math.Ceiling((double)(_pictureHeight - gridOptionsOriginY) / (double)gridOptions.CellSize);
                    fullDeltaX = gridOptionsOriginX % gridOptions.CellSize;
                    fullDeltaY = (_pictureHeight - gridOptionsOriginY) % gridOptions.CellSize;
                    break;
            }

            double deltaX = fullDeltaX * renderScale;
            double deltaY = fullDeltaY * renderScale;
            int lastFullDeltaX = 0;
            int lastFullDeltaY = 0;

            double renderTileSize = (gridOptions.CellSize * renderScale);

            if (colHeaders != null)
            {
                if (deltaX == 0)
                    deltaX = renderTileSize;

                Canvas.SetLeft(colHeaders, Math.Floor(_renderCenter.X - rlPixelWidth / 2.0));

                Border firstCol = (Border)colHeaders.Children[0];
                firstCol.Width = Math.Ceiling(Math.Abs(deltaX));
                double colLeft = 0;
                if ((_rotateAngle == 90) || (_rotateAngle == 180))
                {
                    Canvas.SetLeft(firstCol, rlPixelWidth - firstCol.Width);
                    colLeft = rlPixelWidth - firstCol.Width - renderTileSize;
                }
                else
                {
                    Canvas.SetLeft(firstCol, 0);
                    colLeft = deltaX;
                }

                int colCount = colHeaders.Children.Count;

                UpdateCol(firstCol, deltaX, firstColIndex, colCount);


                for (int c = 1; c < colCount; c++)
                {
                    Border col = (Border)colHeaders.Children[c];


                    if ((_rotateAngle == 90) || (_rotateAngle == 180))
                    {

                        if (colLeft < 0) //Last col
                        {
                            col.Width = Math.Max(0, Math.Ceiling(renderTileSize + colLeft));
                            if (col.Width > 0)
                            {
                                lastFullDeltaX = (int)Math.Ceiling(col.Width / renderScale);
                            }
                            colLeft = 0;
                        }
                        else
                        {
                            col.Width = Math.Ceiling(renderTileSize);
                        }

                        Canvas.SetLeft(col, Math.Floor(colLeft));

                        colLeft -= renderTileSize;
                    }
                    else
                    {
                        Canvas.SetLeft(col, Math.Floor(colLeft));

                        colLeft += renderTileSize;

                        if (colLeft > rlPixelWidth) //Last col
                        {
                            col.Width = Math.Max(0, Math.Ceiling(renderTileSize - (colLeft - rlPixelWidth)));
                            if (col.Width > 0)
                            {
                                lastFullDeltaX = (int)Math.Ceiling(col.Width / renderScale);
                            }
                        }
                        else
                            col.Width = Math.Ceiling(renderTileSize);
                    }

                    UpdateCol(col, renderTileSize, firstColIndex + c, colCount);

                }

                colHeaders.Width = rlPixelWidth;
            }

            if (rowHeaders != null)
            {
                if (deltaY == 0)
                    deltaY = renderTileSize;

                Canvas.SetTop(rowHeaders, Math.Floor(_renderCenter.Y - rlPixelHeight / 2.0));

                Border firstRow = (Border)rowHeaders.Children[0];
                firstRow.Height = Math.Ceiling(Math.Abs(deltaY));
                double rowTop = 0;
                if ((_rotateAngle == 270) || (_rotateAngle == 180))
                {
                    Canvas.SetTop(firstRow, rlPixelHeight - firstRow.Height);
                    rowTop = rlPixelHeight - firstRow.Height - renderTileSize;
                }
                else
                {
                    rowTop = deltaY;
                    Canvas.SetTop(firstRow, 0);
                }

                int rowCount = rowHeaders.Children.Count;


                UpdateRow(firstRow, deltaY, firstRowIndex, rowCount);



                for (int r = 1; r < rowCount; r++)
                {
                    Border row = (Border)rowHeaders.Children[r];


                    if ((_rotateAngle == 180) || (_rotateAngle == 270))
                    {
                        if (rowTop < 0) //Last col
                        {
                            row.Height = Math.Max(0, Math.Ceiling(renderTileSize + rowTop));
                            if (row.Height > 0)
                            {
                                lastFullDeltaY = (int)Math.Ceiling(row.Height / renderScale);
                            }
                            rowTop = 0;
                        }
                        else
                        {
                            row.Height = Math.Ceiling(renderTileSize);
                        }

                        Canvas.SetTop(row, Math.Floor(rowTop));

                        rowTop -= renderTileSize;
                    }
                    else
                    {
                        Canvas.SetTop(row, Math.Floor(rowTop));

                        rowTop += renderTileSize;

                        if (rowTop > rlPixelHeight) //Last col
                        {
                            row.Height = Math.Max(0, Math.Ceiling(renderTileSize - (rowTop - rlPixelHeight)));
                            if (row.Height > 0)
                            {
                                lastFullDeltaY = (int)Math.Ceiling(row.Height / renderScale);
                            }
                        }
                        else
                            row.Height = Math.Ceiling(renderTileSize);
                    }

                    UpdateRow(row, renderTileSize, firstRowIndex + r, rowCount);

                }

                rowHeaders.Height = rlPixelHeight;
            }

            if (gridCanvas != null)
            {
                if (displayGrid)
                    gridParent.Visibility = System.Windows.Visibility.Visible;
                else
                    gridParent.Visibility = System.Windows.Visibility.Collapsed;

                ScaleTransform gridScaleTransform = null;
                double scale = renderScale;
                if (scale == 1.0)
                {
                    gridScaleTransform = null;
                }
                else
                {
                    gridScaleTransform = new ScaleTransform();
                    gridScaleTransform.ScaleX = scale;
                    gridScaleTransform.ScaleY = scale;
                    gridScaleTransform.CenterX = 0;
                    gridScaleTransform.CenterY = 0;
                }


                gridCanvas.RenderTransform = gridScaleTransform;

                Canvas.SetLeft(gridCanvas, _renderCenter.X - rlPixelWidth / 2.0);
                Canvas.SetTop(gridCanvas, _renderCenter.Y - rlPixelHeight / 2.0);

            }

            int nbrColDisplayedToLeftOfOrigin = 0;

            int horizontalDelta = fullDeltaX;
            switch (_rotateAngle)
            {
                case 0:
                    nbrColDisplayedToLeftOfOrigin = (int)Math.Ceiling((double)gridOptionsOriginX / (double)gridOptions.CellSize);
                    horizontalDelta = fullDeltaX;
                    break;

                case 90:
                    nbrColDisplayedToLeftOfOrigin = (int)Math.Ceiling((double)(_pictureWidth - gridOptionsOriginX) / (double)gridOptions.CellSize);
                    horizontalDelta = lastFullDeltaX;
                    break;

                case 180:
                    nbrColDisplayedToLeftOfOrigin = (int)Math.Ceiling((double)(_pictureWidth - gridOptionsOriginX) / (double)gridOptions.CellSize);
                    horizontalDelta = lastFullDeltaX;
                    break;

                case 270:
                    nbrColDisplayedToLeftOfOrigin = (int)Math.Ceiling((double)gridOptionsOriginX / (double)gridOptions.CellSize);
                    horizontalDelta = fullDeltaX;
                    break;
            }

            int reste = nbrColDisplayedToLeftOfOrigin % 2;

            if (gridOptionsOriginX % gridOptions.CellSize == 0)
            {
                if (reste == 0)
                    reste = 1;
                else
                    reste = 0;
            }

            //int reste = 1;
            //if (gridOptionsOriginX % (2 * gridOptions.CellSize) >= gridOptions.CellSize)
            //    reste = 0;


            Rectangle firstVRectangle = (Rectangle)gridCanvas.Children[0];
            if (reste == 1)
                firstVRectangle.Width = horizontalDelta;
            else
                firstVRectangle.Width = 0;

            int rectIndex = 1;
            for (int x = 0, c = 0; x < _pictureWidth; x += gridOptions.CellSize, c++)
            {
                if (c % 2 == reste)
                {
                    Rectangle rectangle = (Rectangle)gridCanvas.Children[rectIndex];
                    rectIndex++;

                    int left = x + horizontalDelta;
                    Canvas.SetLeft(rectangle, left);

                    if (_pictureWidth - left < gridOptions.CellSize)
                        rectangle.Width = Math.Max(0, _pictureWidth - left);
                    else
                        rectangle.Width = gridOptions.CellSize;
                }
            }

            int nbrRowDisplayedToTopOfOrigin = 0;
            int verticalDelta = fullDeltaY;
            switch (_rotateAngle)
            {
                case 0:
                    nbrColDisplayedToLeftOfOrigin = (int)Math.Ceiling((double)gridOptionsOriginY / (double)gridOptions.CellSize);
                    verticalDelta = fullDeltaY;
                    break;

                case 90:
                    nbrColDisplayedToLeftOfOrigin = (int)Math.Ceiling((double)gridOptionsOriginY / (double)gridOptions.CellSize);
                    verticalDelta = fullDeltaY;
                    break;

                case 180:
                    nbrColDisplayedToLeftOfOrigin = (int)Math.Ceiling((double)(_pictureHeight - gridOptionsOriginY) / (double)gridOptions.CellSize);
                    verticalDelta = lastFullDeltaY;
                    break;

                case 270:
                    nbrColDisplayedToLeftOfOrigin = (int)Math.Ceiling((double)(_pictureHeight - gridOptionsOriginY) / (double)gridOptions.CellSize);
                    verticalDelta = lastFullDeltaY;
                    break;
            }

            reste = nbrRowDisplayedToTopOfOrigin % 2;

            if (gridOptionsOriginY % gridOptions.CellSize == 0)
            {
                if (reste == 0)
                    reste = 1;
                else
                    reste = 0;
            }
            //reste = 1;
            //if (gridOptionsOriginY % (2 * gridOptions.CellSize) >= gridOptions.CellSize)
            //    reste = 0;

            if (firstHRectangleIndex >= 0)
            {
                rectIndex = firstHRectangleIndex;
                Rectangle firstHRectangle = (Rectangle)gridCanvas.Children[rectIndex];
                rectIndex++;
                if (reste == 1)
                    firstHRectangle.Height = verticalDelta;
                else
                    firstHRectangle.Height = 0;

                for (int y = 0, c = 0; y < _pictureHeight; y += gridOptions.CellSize, c++)
                {
                    if (c % 2 == reste)
                    {
                        Rectangle rectangle = (Rectangle)gridCanvas.Children[rectIndex];
                        rectIndex++;

                        int top = y + verticalDelta;
                        Canvas.SetTop(rectangle, top);

                        if (_pictureHeight - top < gridOptions.CellSize)
                            rectangle.Height = Math.Max(0, _pictureHeight - top);
                        else
                            rectangle.Height = gridOptions.CellSize;
                    }
                }
            }
        }

        private int GetGridOptionOriginX()
        {
            switch (_rotateAngle)
            {
                case 0:
                    return gridOptions.OriginX;

                case 90:
                    return _pictureWidth - gridOptions.OriginY;

                case 180:
                    return _pictureWidth - gridOptions.OriginX;

                case 270:
                    return gridOptions.OriginY;
            }

            throw new ArgumentException("Invalide rotateAngle value");
        }

        private int GetGridOptionOriginY()
        {
            switch (_rotateAngle)
            {
                case 0:
                    return gridOptions.OriginY;

                case 90:
                    return gridOptions.OriginX;

                case 180:
                    return _pictureHeight - gridOptions.OriginY;

                case 270:
                    return _pictureHeight - gridOptions.OriginX;
            }

            throw new ArgumentException("Invalide rotateAngle value");
        }

        private void UpdateCol(Border col, double renderTileSize, int colIndex, int colCount)
        {
            TextBlock tb = (TextBlock)col.Child;
            if (renderTileSize > 40)
                tb.FontSize = 16;
            else if (renderTileSize > 12)
                tb.FontSize = 10;
            else
                tb.FontSize = 6;

            switch (_rotateAngle)
            {
                case 0:
                    tb.Text = GetRowAlphabeticHeader(colIndex);
                    break;

                case 90:
                    tb.Text = colIndex.ToString(); //(colCount - 2 - colIndex).ToString();
                    break;

                case 180:
                    tb.Text = GetRowAlphabeticHeader(colIndex); //GetRowAlphabeticHeader(colCount - 2 - colIndex);
                    break;

                case 270:
                    tb.Text = colIndex.ToString();
                    break;
            }
        }

        private void UpdateRow(Border row, double renderTileSize, int rowIndex, int rowCount)
        {
            TextBlock tb = (TextBlock)row.Child;
            if (renderTileSize > 40)
                tb.FontSize = 16;
            else if (renderTileSize > 12)
                tb.FontSize = 10;
            else
                tb.FontSize = 6;

            tb.Text = rowIndex.ToString();
            switch (_rotateAngle)
            {
                case 0:
                    tb.Text = rowIndex.ToString();
                    break;

                case 90:
                    tb.Text = GetRowAlphabeticHeader(rowIndex);
                    break;

                case 180:
                    tb.Text = rowIndex.ToString(); //(rowCount - 2 - rowIndex).ToString();
                    break;

                case 270:
                    tb.Text = GetRowAlphabeticHeader(rowIndex); //GetRowAlphabeticHeader(rowCount - 2 - rowIndex);
                    break;
            }

        }

        //void img_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        //{
        //    MessageBox.ShowError("An error occured while loading an image", "Error", e.ErrorException);
        //}

        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }

        private void HideTiles()
        {
            foreach (Layer l in _layers)
            {
                Tile[] tiles = l.Tiles;
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i] != null)
                        tiles[i].Image.Visibility = Visibility.Collapsed;
                }
            }
        }

        private Note currentNote;
        public Note CurrentNote
        {
            get { return currentNote; }
            set
            {
                if (currentNote != value)
                {
                    if (currentNote != null)
                    {
                        currentNote.Documents.CollectionChanged -= Documents_CollectionChanged;
                        if (CurrentNotifyRectangle != null)
                        {
                            CurrentNotifyRectangle.Dispose();
                            CurrentNotifyRectangle = null;
                        }
                        ClearCurrentNotifShapeList();
                    }

                    currentNote = value;

                    if (currentNote != null)
                    {
                        currentNote.Documents.CollectionChanged += Documents_CollectionChanged;
                        RefreshCurrentNoteDisplay();
                        UpdateDrawingCanvas();
                    }

                    _imageLoadedComplete = false;
                    OnCurrentNoteChanged(EventArgs.Empty);
                }
            }
        }

        void Documents_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RefreshCurrentNoteDisplay();
        }

        public Drawing CurrentDrawing
        {
            get
            {
                if (currentNote != null && currentNote.Documents.Count > 0 && currentNote.Messages.Count > 0 && currentNote.Messages[0].Comments.Count > 0)
                {
                    NoteDocument currentDocument = currentNote.GetNoteDocument(this.PlanId);
                    if (currentDocument == null)
                        return null;

                    Drawing currentDrawing = currentNote.Messages[0].Comments[0].GetDrawing(currentDocument.GuidId);
                    if (currentDrawing == null)
                    {
                        currentDrawing = new Drawing();
                        currentDrawing.NoteDocumentGuidID = currentDocument.GuidId;
                        currentDrawing.NoteCommentID = currentNote.Messages[0].Comments[0].Id;
                        currentDrawing.PageIndex = currentNote.Messages[0].Comments[0].PageIndex;
                        currentNote.Messages[0].Comments[0].Drawings.Add(currentDrawing);
                    }

                    return currentDrawing;
                }
                return null;
            }
        }

        public void RefreshCurrentNoteDisplay()
        {
            if (currentNote == null)
                return;

            CurrentElement = null;

            Drawing currentDrawing = CurrentDrawing;
            if (currentDrawing == null) return;

            if (currentDrawing.Scale == 0)
                currentDrawing.Scale = 1;

            if (currentDrawing.HasRectangle || (currentDrawing.Shapes.Count > 0))
            {
                UnselectCurrentElement();
                ClearCurrentNotifShapeList();
                //CurrentElement = null;
                if (CurrentNotifyRectangle != null)
                {
                    CurrentNotifyRectangle.Dispose();
                }


                double currentDrawingRenderCenterX = currentDrawing.RenderCenterX;
                double currentDrawingRenderCenterY = currentDrawing.RenderCenterY;
                double currentDrawingScale = currentDrawing.Scale;
                int afterZoomRenderLevel = 0;
                bool isDrawingScaleTooSmall = false;
                if (currentDrawingScale > 0)
                {
                    afterZoomRenderLevel = GetRenderLevel(currentDrawingScale, out isDrawingScaleTooSmall);
                    if (isDrawingScaleTooSmall)
                    {
                        currentDrawingScale = GetScale(afterZoomRenderLevel);
                        double deltaScale = currentDrawing.Scale / currentDrawingScale;
                        currentDrawingRenderCenterX *= deltaScale; //currentDrawingRenderCenterX /= 2.0;
                        currentDrawingRenderCenterY *= deltaScale; //currentDrawingRenderCenterY /= 2.0;
                    }
                }

                double currentDrawingLeft = currentDrawing.Left;
                double currentDrawingTop = currentDrawing.Top;
                double currentDrawingRight = currentDrawing.Left + currentDrawing.Width;
                double currentDrawingBottom = currentDrawing.Top + currentDrawing.Height;
                if (currentDrawing.HasRectangle)
                {
                    CurrentNotifyRectangle = new BlinkerRectangle(currentNote, currentDrawing.NoteDocumentGuidID); //currentNote.DrawingRectangle;
                }
                else
                {
                    CurrentNotifyRectangle = null;
                    currentDrawingLeft = double.MaxValue;
                    currentDrawingTop = double.MaxValue;
                    currentDrawingRight = -double.MaxValue;
                    currentDrawingBottom = -double.MaxValue;
                    foreach (NoteShape notificationShape in currentDrawing.Shapes)
                    {
                        FrameworkElement fe = notificationShape.CreateFrameworkElement(controlIndex.ToString());

                        WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(fe, this, (ls, s, e) => ls.currentShape_MouseLeftButtonDown(s, e));

                        _currentNotifyElementList.Add(fe);

                        double shapeLeft = double.MaxValue;
                        double shapeTop = double.MaxValue;
                        double shapeRight = -double.MaxValue;
                        double shapeBottom = -double.MaxValue;

                        //Coordonnées et tailles par rapport au canvas qui contient la shape
                        NoteFlatShape flatShape = notificationShape as NoteFlatShape;
                        if (flatShape != null)
                        {
                            shapeLeft = flatShape.X;
                            shapeTop = flatShape.Y;
                            shapeRight = flatShape.Width + shapeLeft;
                            shapeBottom = flatShape.Height + shapeTop;
                        }
                        else
                        {
                            NoteLine lineShape = notificationShape as NoteLine;
                            if (lineShape != null)
                            {
                                double x1 = lineShape.X1;
                                double x2 = lineShape.X2;
                                double y1 = lineShape.Y1;
                                double y2 = lineShape.Y2;

                                if (x1 > x2)
                                {
                                    shapeLeft = x2;
                                    shapeRight = x1;
                                }
                                else
                                {
                                    shapeLeft = x1;
                                    shapeRight = x2;
                                }

                                if (y1 > y2)
                                {
                                    shapeTop = y2;
                                    shapeBottom = y1;
                                }
                                else
                                {
                                    shapeTop = y1;
                                    shapeBottom = y2;
                                }

                            }
                            else
                            {
                                NotePolyline polyLineShape = notificationShape as NotePolyline;
                                if (polyLineShape != null)
                                {
                                    foreach (Point point in polyLineShape.Points)
                                    {
                                        if (point.X < shapeLeft)
                                            shapeLeft = point.X;

                                        if (point.X > shapeRight)
                                            shapeRight = point.X;

                                        if (point.Y < shapeTop)
                                            shapeTop = point.Y;

                                        if (point.Y > shapeBottom)
                                            shapeBottom = point.Y;
                                    }
                                }
                                else
                                {
                                    NoteText text = notificationShape as NoteText;
                                    if (text != null)
                                    {
                                        double x = text.X;
                                        double y = text.Y;

                                        shapeLeft = x;
                                        shapeRight = x + 10;
                                        shapeTop = y;
                                        shapeBottom = y + 10;
                                    }
                                    else
                                    {
                                        NotePin handoverPinShape = notificationShape as NotePin;
                                        if (handoverPinShape != null)
                                        {
                                            double x = handoverPinShape.X;
                                            double y = handoverPinShape.Y;

                                            shapeLeft = x;
                                            shapeRight = x + PIN_WIDTH; // * currentDrawingScale);
                                            shapeTop = y - PIN_HEIGHT; // * currentDrawingScale ICI);  
                                            shapeBottom = y;
                                        }
                                        else
                                            throw new InvalidCastException("Invalid shape");
                                    }
                                }
                            }
                        }

                        if (shapeLeft < currentDrawingLeft)
                            currentDrawingLeft = shapeLeft;

                        if (shapeTop < currentDrawingTop)
                            currentDrawingTop = shapeTop;

                        if (shapeRight > currentDrawingRight)
                            currentDrawingRight = shapeRight;

                        if (shapeBottom > currentDrawingBottom)
                            currentDrawingBottom = shapeBottom;

                    }
                }



                Point currentNotifRenderCenter = RotateViewPortPoint(new Point(currentDrawingRenderCenterX, currentDrawingRenderCenterY));

                //ICI
                double currentNoteRenderCenterX = currentNotifRenderCenter.X;
                double currentNoteRenderCenterY = currentNotifRenderCenter.Y;
                if ((_layers.Count > 0) && (this.Visibility == System.Windows.Visibility.Visible) && (currentDrawing.Scale > 0))
                {


                    int afterZoomPixelWidth = _layers[afterZoomRenderLevel].PixelWidth;
                    int afterZoomPixelHeight = _layers[afterZoomRenderLevel].PixelHeight;
                    if ((_rotateAngle == 90) || (_rotateAngle == 270))
                    {
                        afterZoomPixelWidth = _layers[afterZoomRenderLevel].PixelHeight;
                        afterZoomPixelHeight = _layers[afterZoomRenderLevel].PixelWidth;
                    }

                    double currentNoteRight = currentDrawingRight * currentDrawingScale + currentDrawingRenderCenterX - afterZoomPixelWidth / 2; // - currentNote.RenderCenterX;
                    double currentNoteBottom = currentDrawingBottom * currentDrawingScale + currentDrawingRenderCenterY - afterZoomPixelHeight / 2;
                    double currentNoteLeft = currentDrawingLeft * currentDrawingScale + currentDrawingRenderCenterX - afterZoomPixelWidth / 2; // - currentNote.RenderCenterX;
                    double currentNoteTop = currentDrawingTop * currentDrawingScale + currentDrawingRenderCenterY - afterZoomPixelHeight / 2;


                    double afterZoomRight = this.ActualWidth;
                    double afterZoomBottom = this.ActualHeight;
                    if ((_rotateAngle == 90) || (_rotateAngle == 270))
                    {
                        afterZoomRight = this.ActualHeight;
                        afterZoomBottom = this.ActualWidth;
                    }

                    if (currentNoteRight > afterZoomRight - 10)
                    {
                        double delta = (currentNoteRight - afterZoomRight + 10);
                        currentNoteLeft -= delta;
                        currentNoteRight -= delta;
                        currentDrawingRenderCenterX -= delta;
                        switch (_rotateAngle)
                        {
                            case 0:
                                currentNoteRenderCenterX -= delta;
                                break;

                            case 90:
                                currentNoteRenderCenterY -= delta;
                                break;

                            case 180:
                                currentNoteRenderCenterX += delta;
                                break;

                            case 270:
                                currentNoteRenderCenterY += delta;
                                break;
                        }
                    }
                    else if (currentNoteLeft < 0)
                    {
                        double delta = currentNoteLeft - 10;
                        currentNoteLeft -= delta;
                        currentNoteRight -= delta;
                        currentDrawingRenderCenterX -= delta;

                        switch (_rotateAngle)
                        {
                            case 0:
                                currentNoteRenderCenterX -= delta;
                                break;

                            case 90:
                                currentNoteRenderCenterY -= delta;
                                break;

                            case 180:
                                currentNoteRenderCenterX += delta;
                                break;

                            case 270:
                                currentNoteRenderCenterY += delta;
                                break;
                        }
                    }


                    if (currentNoteBottom > afterZoomBottom - 10)
                    {
                        double delta = (currentNoteBottom - afterZoomBottom + 10);
                        currentNoteTop -= delta;
                        currentNoteBottom -= delta;
                        currentDrawingRenderCenterY -= delta;

                        switch (_rotateAngle)
                        {
                            case 0:
                                currentNoteRenderCenterY -= delta;
                                break;

                            case 90:
                                currentNoteRenderCenterX += delta;
                                break;

                            case 180:
                                currentNoteRenderCenterY += delta;
                                break;

                            case 270:
                                currentNoteRenderCenterX -= delta;
                                break;
                        }
                    }
                    else if (currentNoteTop < 0)
                    {
                        double delta = currentNoteTop - 10;
                        currentNoteTop -= delta;
                        currentNoteBottom -= delta;
                        currentDrawingRenderCenterY -= delta;
                        switch (_rotateAngle)
                        {
                            case 0:
                                currentNoteRenderCenterY -= delta;
                                break;

                            case 90:
                                currentNoteRenderCenterX += delta;
                                break;

                            case 180:
                                currentNoteRenderCenterY += delta;
                                break;

                            case 270:
                                currentNoteRenderCenterX -= delta;
                                break;
                        }
                    }

                    //Same white space at the top and at the bottom. Same white space at the left and at the right
                    double pictureLeft = currentDrawingRenderCenterX - afterZoomPixelWidth / 2;
                    double pictureRight = currentDrawingRenderCenterX + afterZoomPixelWidth / 2;

                    double actualWidth = this.ActualWidth;
                    double actualHeight = this.ActualHeight;
                    if ((_rotateAngle == 90) || (_rotateAngle == 270))
                    {
                        actualWidth = this.ActualHeight;
                        actualHeight = this.ActualWidth;
                    }

                    double deltaXLeft = 0;
                    if (pictureLeft > 0)
                    {
                        if (pictureLeft < currentNoteLeft - 10)
                        {
                            deltaXLeft = pictureLeft;
                        }
                        else
                        {
                            deltaXLeft = currentNoteLeft - 10;
                        }
                    }

                    double deltaXRight = 0;
                    if (pictureRight < actualWidth)
                    {
                        if (pictureRight > currentNoteRight + 10)
                        {
                            deltaXRight = actualWidth - pictureRight;
                        }
                        else
                        {
                            deltaXRight = actualWidth - (currentNoteRight + 10);
                        }
                    }

                    double deltaH = 0;
                    if ((deltaXLeft > 0) && (deltaXRight > 0))
                    {
                        deltaH = (deltaXRight - deltaXLeft) / 2;
                    }
                    else if (deltaXLeft > 0)
                    {
                        double extraRight = pictureRight - actualWidth;
                        if (extraRight > deltaXLeft)
                        {
                            deltaH = deltaXLeft;
                        }
                        else
                        {
                            deltaH = extraRight + (deltaXLeft - extraRight) / 2;
                        }

                    }
                    else if (deltaXRight > 0)
                    {
                        double extraLeft = -pictureLeft;
                        if (extraLeft > deltaXRight)
                        {
                            deltaH = -deltaXRight;
                        }
                        else
                        {
                            deltaH = -(extraLeft + (deltaXRight - extraLeft) / 2);
                        }
                    }


                    switch (_rotateAngle)
                    {
                        case 0:
                            currentNoteRenderCenterX -= deltaH;
                            break;

                        case 90:
                            currentNoteRenderCenterY -= deltaH;
                            break;

                        case 180:
                            currentNoteRenderCenterX += deltaH;
                            break;

                        case 270:
                            currentNoteRenderCenterY += deltaH;
                            break;
                    }

                    double pictureTop = currentDrawingRenderCenterY - afterZoomPixelHeight / 2;
                    double pictureBottom = currentDrawingRenderCenterY + afterZoomPixelHeight / 2;

                    double deltaYTop = 0;
                    if (pictureTop > 0)
                    {
                        if (pictureTop < currentNoteTop - 10)
                        {
                            deltaYTop = pictureTop;
                        }
                        else
                        {
                            deltaYTop = currentNoteTop - 10;
                        }
                    }

                    double deltaYBottom = 0;
                    if (pictureBottom < actualHeight)
                    {
                        if (pictureBottom > currentNoteBottom + 10)
                        {
                            deltaYBottom = actualHeight - pictureBottom;
                        }
                        else
                        {
                            deltaYBottom = actualHeight - (currentNoteBottom + 10);
                        }
                    }

                    double deltaV = 0;
                    if ((deltaYTop > 0) && (deltaYBottom > 0))
                    {
                        deltaV = (deltaYTop - deltaYBottom) / 2;
                    }
                    else if (deltaYTop > 0)
                    {
                        double extraBottom = pictureBottom - actualHeight;
                        if (extraBottom > deltaYTop)
                        {
                            deltaV = deltaYTop;
                        }
                        else
                        {
                            deltaV = extraBottom + (deltaYTop - extraBottom) / 2;
                        }

                    }
                    else if (deltaYBottom > 0)
                    {
                        double extraTop = -pictureTop;
                        if (extraTop > deltaYBottom)
                        {
                            deltaV = -deltaYBottom;
                        }
                        else
                        {
                            deltaV = -(extraTop + (deltaYBottom - extraTop) / 2);
                        }
                    }


                    switch (_rotateAngle)
                    {
                        case 0:
                            currentNoteRenderCenterY -= deltaV;
                            break;

                        case 90:
                            currentNoteRenderCenterX += deltaV;
                            break;

                        case 180:
                            currentNoteRenderCenterY += deltaV;
                            break;

                        case 270:
                            currentNoteRenderCenterX -= deltaV;
                            break;
                    }


                    //if (afterZoomPixelWidth < ActualWidth)
                    //{
                    //}
                }


                Zoom(currentDrawingScale, currentNoteRenderCenterX, currentNoteRenderCenterY);
            }
            else if (((NotifyType == MaVuViecClient.NotifyType.None) || (NotifyType == MaVuViecClient.NotifyType.Draw)) && !currentNote.IsNew)
            {
                int choosenLevel = -1;
                int maxLevel = -1;
                foreach (Layer layer in _layers)
                {
                    maxLevel = layer.Level;
                    if ((layer.PixelWidth < ActualWidth) && (layer.PixelHeight < ActualHeight))
                    {
                        choosenLevel = layer.Level;
                        break;
                    }
                }

                if (choosenLevel == -1)
                {
                    choosenLevel = maxLevel;
                }

                RenderLevel = choosenLevel;
                _renderCenter.X = this.ActualWidth / 2;
                _renderCenter.Y = this.ActualHeight / 2;
                UpdateTiles(this.ActualWidth, this.ActualHeight);
            }
            RefreshTransformPin();
        }


        protected void OnCurrentNoteChanged(EventArgs e)
        {
            if (CurrentNoteChanged != null)
                CurrentNoteChanged(this, e);
        }


        private BlinkerRectangle CurrentNotifyRectangle
        {
            get { return _currentNotifyRectangle; }
            set
            {
                if (_currentNotifyRectangle != value)
                {
                    if (_currentNotifyRectangle != null)
                        _currentNotifyRectangle.IsBlinking = false;

                    if (drawingCanvas != null)
                    {
                        drawingCanvas.Children.Clear();
                        _shapeMover = null;
                        _shapeHandler1 = null;
                        _shapeHandler2 = null;
                        ClearCurrentNotifShapeList();
                    }

                    _currentNotifyRectangle = value;

                    if (_currentNotifyRectangle != null)
                    {
                        if (drawingCanvas != null)
                            drawingCanvas.Children.Add(CurrentNotifyRectangle);
                        _currentNotifyRectangle.IsBlinking = true;
                    }
                }
            }
        }

        private ObservableCollection<FrameworkElement> CurrentNotifyShapeList
        {
            get { return _currentNotifyElementList; }
        }

        void currentNotifyElementList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (drawingCanvas != null)
                    {
                        int insertIndex = e.NewStartingIndex;
                        foreach (FrameworkElement shape in e.NewItems)
                        {
                            if (drawingCanvas.Children.Count == 0)
                                drawingCanvas.Children.Add(shape);
                            else
                                drawingCanvas.Children.Insert(insertIndex, shape);
                            insertIndex++;
                        }
                    }
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (drawingCanvas != null)
                    {
                        int deleteIndex = e.OldStartingIndex;
                        for (int i = 0; i < e.OldItems.Count; i++)
                        {
                            drawingCanvas.Children.RemoveAt(deleteIndex);
                        }
                    }
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    if (drawingCanvas != null)
                    {
                        drawingCanvas.Children.Clear();
                        _shapeMover = null;
                        _shapeHandler1 = null;
                        _shapeHandler2 = null;
                        currentItemPin = null;
                    }
                    break;
            }
        }

        /// <summary>
        /// to reset new point
        /// </summary>
        public void SetNewPoint()
        {
            currentItemPin = null;
        }

        internal void SetPin(bool isNew)
        {
            if (this.ActualHeight == 0 || this.ActualHeight == 0)
                return;

            if (_currentNotifyElementList != null && _currentNotifyElementList.Count > 0)
            {
                foreach (FrameworkElement noteShape in _currentNotifyElementList)
                {
                    ItemPin pin = noteShape as ItemPin;
                    if (pin != null)
                    {
                        currentItemPin = pin;
                        CurrentElement = currentItemPin;
                    }
                }
            }

            if (isNew || currentItemPin == null)
            {
                currentItemPin = new ItemPin();
                WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(currentItemPin, this, (ls, s, we) => ls.currentShape_MouseLeftButtonDown(s, we));
                currentItemPin.Tag = "Pin";
                currentItemPin.Name = Guid.NewGuid().ToString() + controlIndex.ToString();
            }
            RotateTransform itPinTransform = new RotateTransform();
            itPinTransform.Angle = -_rotateAngle;
            currentItemPin.RenderTransform = itPinTransform;

            if (!_currentNotifyElementList.Contains(currentItemPin))
            {
                _currentNotifyElementList.Add(currentItemPin);
                CurrentDrawing.Shapes.Add((new NotePin(currentItemPin, notifyDrawBrushColor.Color, (int)(NotifyDrawBrushWidth), controlIndex.ToString())));
            }
            CurrentElement = currentItemPin;
            Canvas.SetLeft(CurrentElement, _renderCenter.X);
            Canvas.SetTop(CurrentElement, _renderCenter.Y);
            //ShowCurrentShapeMover();
        }



        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (!isUserInteractionEnabled)
                return;

            if (e.Handled)
                return;

            if (AddLinks || IsSelectedArea)
            {
                _mouseDownActive = true;
                _mouseDownInitialRenderCenter = _renderCenter;
                _mouseDownInitialLocation = e.GetPosition(this);
                double scale = GetCurrentScale();
                Point clickRelativeLocation = InvertRotateImagePoint(GetRelativeViewPortLocation(_mouseDownInitialLocation));

                if (IsOverlapsRectangles(clickRelativeLocation.Y, 1))
                {
                    _mouseDownActive = false;
                    base.OnMouseLeftButtonDown(e);
                    return;
                }

                Rectangle newShape = new Rectangle();
                newShape.Tag = "Rectangle";
                newShape.Cursor = Cursors.Arrow;
                newShape.Name = Guid.NewGuid().ToString() + controlIndex.ToString();
                newShape.Width = 1;
                newShape.Height = 1;

                //Canvas.SetLeft(newShape, clickRelativeLocation.X);                
                var imageLeft = Math.Floor(_renderCenter.X - (_layers[RenderLevel].PixelWidth / 2.0));
                double xx = imageLeft - Canvas.GetLeft(selectedAreaCanvas);// / GetCurrentScale();
                Canvas.SetLeft(newShape, xx);
                Canvas.SetTop(newShape, clickRelativeLocation.Y);

                SolidColorBrush brush = new SolidColorBrush(Colors.Yellow);
                brush.Opacity = 0.3;
                newShape.Fill = brush;
                newShape.StrokeThickness = 1;
                newShape.Stroke = new SolidColorBrush(Colors.Black);

                newShape.Width = _pictureWidth; /// GetCurrentScale();/// Canvas.GetLeft(this.colHeaders) - Canvas.GetLeft(newShape) + this.colHeaders.Width;

                RectSelectedArea = newShape;

                base.OnMouseLeftButtonDown(e);
                return;
            }

            if (isInClickOrigine)
            {
                isInClickOrigine = false;
                this.Cursor = System.Windows.Input.Cursors.Hand;
                if (OrigineClicked != null)
                {
                    double scale = GetCurrentScale();

                    Point mouseClickLocation = e.GetPosition(this);
                    Point clickRelativeLocation = InvertRotateImagePoint(GetRelativeViewPortLocation(mouseClickLocation));
                    OrigineClicked(this, new OrigineClickedEventArgs((int)Math.Round(clickRelativeLocation.X), (int)Math.Round(clickRelativeLocation.Y)));
                }

                base.OnMouseLeftButtonDown(e);

                return;
            }

            if (_shapeHandler1IsInMove || shapeHandler2IsInMove || _shapeMoverIsInMove)
            {
                base.OnMouseLeftButtonDown(e);
                return;
            }

            //if (NotifyType != MaVuViecClient.NotifyType.None) //(NotifyType != MaVuViecClient.NotifyType.DrawArrow)
            //{
            if (CurrentElement != null)
            {
                if ((NotifyType != MaVuViecClient.NotifyType.DrawPolyLine) || (CurrentElement.Tag.ToString() != "Polyline"))
                {
                    UnselectCurrentElement();
                }
            }

            if (NotifyType != NotifyType.DrawText)
            {
                this.CaptureMouse();
                _mouseDownActive = true;
            }

            _mouseDownInitialRenderCenter = _renderCenter;
            _mouseDownInitialLocation = e.GetPosition(this);
            //}

            if ((NotifyType != NotifyType.None) && (NotifyType != NotifyType.Draw))// && (NotifyType != MaVuViecClient.NotifyType.DrawArrow) )
            {
                //If we draw on another page, we clear the existing drawing
                if (CurrentDrawing.PageIndex != this._pageIndex)
                {
                    CurrentDrawing.Shapes.Clear();
                    CurrentDrawing.PageIndex = _pageIndex;
                    _currentNotifyElementList.Clear();
                    UpdateDrawingCanvas();
                }

                double scale = GetCurrentScale();
                Point clickRelativeLocation = InvertRotateImagePoint(GetRelativeViewPortLocation(_mouseDownInitialLocation));

                if (NotifyType == NotifyType.Crop)
                {
                    if (CurrentNotifyRectangle != null)
                    {
                        CurrentNotifyRectangle.Dispose();
                    }
                    CurrentNotifyRectangle = new BlinkerRectangle(clickRelativeLocation.X, clickRelativeLocation.Y, 1, 1, scale, Guid.NewGuid());
                }
                else if (NotifyType == NotifyType.DrawText)
                {
                    TextBox textBox = new TextBox();
                    textBox.Tag = "TextBox";
                    textBox.Foreground = notifyDrawBrushColor;
                    textBox.AcceptsReturn = true;
                    textBox.CaretBrush = new SolidColorBrush(notifyDrawBrushColor.Color);
                    textBox.Name = Guid.NewGuid() + controlIndex.ToString();

                    RotateTransform txtTransform = new RotateTransform();
                    txtTransform.Angle = -_rotateAngle;
                    textBox.RenderTransform = txtTransform;

                    Canvas.SetLeft(textBox, clickRelativeLocation.X);
                    Canvas.SetTop(textBox, clickRelativeLocation.Y);
                    textBox.FontSize = (int)(NotifyDrawBrushWidth);
                    WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(textBox, this, (ls, s, we) => ls.currentShape_MouseLeftButtonDown(s, we));

                    WeakEventHelper.RegisterTextBoxTextChanged<PictureViewer>(textBox, this, (ls, s, we) => ls.textBox_TextChanged(s, we));

                    CurrentElement = textBox;
                    textBox.Text = "";
                    _currentNotifyElementList.Add(textBox);
                    textBox.Focus();

                    ShowCurrentShapeMover();

                    CurrentDrawing.Shapes.Add(new NoteText(textBox, notifyDrawBrushColor.Color, (int)(NotifyDrawBrushWidth), controlIndex.ToString()));
                }
                else if (NotifyType == NotifyType.DrawPin)
                {
                    SetPin(false);
                    RotatePin(clickRelativeLocation);

                    NoteShape notificationShape = GetNoteShape(CurrentDrawing, CurrentElement.Name);
                    NotePin pin = notificationShape as NotePin;
                    if (pin != null)
                    {
                        pin.X = clickRelativeLocation.X < 0 ? 0 : clickRelativeLocation.X > _pictureWidth ? _pictureWidth : clickRelativeLocation.X;
                        pin.Y = clickRelativeLocation.Y < 0 ? 0 : clickRelativeLocation.Y > _pictureHeight ? _pictureHeight : clickRelativeLocation.Y;
                    }

                }
                else
                {
                    Shape newShape = null;
                    if ((NotifyType == NotifyType.DrawRectangle) || (NotifyType == NotifyType.DrawElipse) || (NotifyType == NotifyType.DrawCloud))
                    {
                        if (NotifyType == NotifyType.DrawRectangle)
                        {
                            newShape = new Rectangle();
                            newShape.Tag = "Rectangle";
                        }
                        else if (NotifyType == NotifyType.DrawElipse)
                        {
                            newShape = new Ellipse();
                            newShape.Tag = "Ellipse";
                        }
                        else if (NotifyType == MaVuViecClient.NotifyType.DrawCloud)
                        {
                            string pathXaml = "<Path xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" Data=\"M514.5,138 C513.09113,155.82594 492.4996,211.37463 456.56805,205.07205 C455.85007,204.94611 454.905,205.71501 454.86127,206.44499 C452.49973,245.87468 369.00027,269.37473 353.50034,248.37437 C353.22943,248.00732 352.00812,248.68272 352.0697,249.1357 C355.50031,274.37433 266.00052,282.3743 260.50052,254.8746 C260.33694,254.05672 259.85406,253.61749 259.50055,254.375 C245.50056,284.37427 150.50078,271.375 157.50076,245.875 C157.96425,244.18655 156.90549,244.96233 156.38373,245.39565 C126.8838,269.89566 58.000992,239.875 71.000961,212.375 C71.151474,212.05661 70.574173,210.14751 70.267975,210.3192 C34.501331,230.375 -6.7319369,158.8192 33.267521,141.8192 C33.571804,141.68988 32.632538,139.17863 32.500385,138.875 C22.000385,114.75 57.500984,51.374458 104.84062,76.08976 C105.37374,76.368095 106.64218,75.961502 106.50086,75.374489 C93.500893,21.374426 211.49937,-16.754738 256.49875,9.8702335 C257.07352,10.210313 257.94064,10.236294 258.50049,9.8743181 C293.49921,-12.755131 461.50009,8.8746433 462.41492,55.000965 C462.43152,55.837437 463.61838,56.473366 464.44818,56.455444 C514.49994,55.37439 516.99951,106.37447 514.5,138 z\" Stretch=\"Fill\"/>";
                            Path path = (Path)System.Windows.Markup.XamlReader.Load(pathXaml);

                            newShape = path;
                            newShape.Tag = "Cloud";
                        }

                        newShape.Cursor = Cursors.Arrow;
                        newShape.Name = Guid.NewGuid().ToString() + controlIndex.ToString();
                        newShape.Width = 1;
                        newShape.Height = 1;


                        Canvas.SetLeft(newShape, clickRelativeLocation.X);
                        Canvas.SetTop(newShape, clickRelativeLocation.Y);

                        _currentNotifyElementList.Add(newShape);

                        if (NotifyType == NotifyType.DrawRectangle)
                        {
                            CurrentDrawing.Shapes.Add(new NoteRectangle(newShape, notifyDrawBrushColor.Color, (int)(NotifyDrawBrushWidth), controlIndex.ToString(), notifyLineFormatType));
                        }
                        else if (NotifyType == NotifyType.DrawElipse)
                        {
                            CurrentDrawing.Shapes.Add(new NoteElipse(newShape, notifyDrawBrushColor.Color, (int)(NotifyDrawBrushWidth), controlIndex.ToString(), notifyLineFormatType));
                        }
                        else if (NotifyType == NotifyType.DrawCloud)
                        {
                            CurrentDrawing.Shapes.Add(new NoteCloud(newShape, notifyDrawBrushColor.Color, (int)(NotifyDrawBrushWidth), controlIndex.ToString(), notifyLineFormatType));
                        }
                    }
                    else if (NotifyType == NotifyType.DrawLine)
                    {
                        //var pattern = new DoubleCollection();
                        //pattern.Add(1); pattern.Add(2);

                        Line line = new Line();
                        line.Cursor = Cursors.Arrow;
                        newShape = line;
                        line.X1 = clickRelativeLocation.X;
                        line.X2 = clickRelativeLocation.X;
                        line.Y1 = clickRelativeLocation.Y;
                        line.Y2 = clickRelativeLocation.Y;
                        //line.StrokeDashArray = GetStrokeDashByLineFormat(notifyLineFormatType);

                        newShape.Tag = "Line";
                        newShape.Name = Guid.NewGuid().ToString() + controlIndex;

                        _currentNotifyElementList.Add(newShape);

                        CurrentDrawing.Shapes.Add(new NoteLine(line, notifyDrawBrushColor.Color, (int)(NotifyDrawBrushWidth), controlIndex.ToString(), notifyLineFormatType));
                    }
                    else if ((NotifyType == NotifyType.DrawFree) || (NotifyType == NotifyType.DrawPolyLine))
                    {
                        Polyline polyline = null;
                        if (NotifyType == NotifyType.DrawPolyLine)
                        {
                            polyline = _currentElement as Polyline;
                        }
                        if (polyline == null)
                        {
                            polyline = new Polyline();
                            newShape = polyline;
                            polyline.Points.Add(new Point(clickRelativeLocation.X, clickRelativeLocation.Y));
                            newShape.Tag = "Polyline";
                            newShape.Name = Guid.NewGuid().ToString() + controlIndex.ToString();

                            _currentNotifyElementList.Add(newShape);

                            CurrentDrawing.Shapes.Add(new NotePolyline(polyline, notifyDrawBrushColor.Color, (int)(NotifyDrawBrushWidth), controlIndex.ToString(), notifyLineFormatType));
                        }
                        else
                        {
                            newShape = polyline;
                        }

                        polyline.Cursor = Cursors.Arrow;
                        _currentPolyLinePoint = new Point(clickRelativeLocation.X, clickRelativeLocation.Y);
                        polyline.Points.Add(_currentPolyLinePoint);

                        NotePolyline notificationPolyline = (NotePolyline)GetNoteShape(CurrentDrawing, newShape.Name);
                        notificationPolyline.Points.Add(_currentPolyLinePoint);

                    }

                    if (newShape != null)
                    {
                        newShape.Stroke = notifyDrawBrushColor;
                        newShape.StrokeThickness = (int)(NotifyDrawBrushWidth);// / scale);
                        newShape.StrokeDashArray = NoteShape.GetStrokeDashByLineFormat(this.NotifyLineFormatType);
                        WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(newShape, this, (ls, s, we) => ls.currentShape_MouseLeftButtonDown(s, we));
                    }

                    CurrentElement = newShape;
                }


            }

            base.OnMouseLeftButtonDown(e);
        }

        private void RotatePin(Point clickRelativeLocation)
        {
            double left = 0;
            double top = 0;
            switch (_rotateAngle)
            {
                case 0:
                    left = clickRelativeLocation.X < 0 ? 0 - 7 : clickRelativeLocation.X > _pictureWidth ? _pictureWidth - 7 : clickRelativeLocation.X - 7;
                    top = clickRelativeLocation.Y < 0 ? 0 - currentItemPin.PixelHeight + 4 : clickRelativeLocation.Y > _pictureHeight ? _pictureHeight - currentItemPin.PixelHeight + 4 : clickRelativeLocation.Y - currentItemPin.PixelHeight + 4;
                    break;
                case 90:
                    left = clickRelativeLocation.X < 0 ? 0 - currentItemPin.PixelHeight : clickRelativeLocation.X > _pictureHeight ? _pictureHeight - currentItemPin.PixelHeight : clickRelativeLocation.X - currentItemPin.PixelHeight;
                    top = clickRelativeLocation.Y + 4;
                    break;
                case 180:
                    left = clickRelativeLocation.X < 0 ? 0 + 7 : clickRelativeLocation.X > _pictureWidth ? _pictureWidth + 7 : clickRelativeLocation.X + 7;
                    top = clickRelativeLocation.Y < 0 ? 0 + currentItemPin.PixelHeight - 4 : clickRelativeLocation.Y > _pictureHeight ? _pictureHeight + currentItemPin.PixelHeight - 4 : clickRelativeLocation.Y + currentItemPin.PixelHeight - 4;
                    break;
                case 270:
                    left = clickRelativeLocation.X < 0 ? 0 + currentItemPin.PixelHeight : clickRelativeLocation.X > _pictureHeight ? _pictureHeight + currentItemPin.PixelHeight : clickRelativeLocation.X + currentItemPin.PixelHeight;
                    top = clickRelativeLocation.Y - 4;
                    break;
            }
            Canvas.SetLeft(currentItemPin, left);
            Canvas.SetTop(currentItemPin, top);
        }

        private Point RotateViewPortPoint(Point point)
        {

            Point result = new Point();
            switch (_rotateAngle)
            {
                case 0:
                    result.X = point.X;
                    result.Y = point.Y;
                    break;

                case 90:
                    result.X = this.ActualWidth - point.Y;
                    result.Y = point.X;

                    break;

                case 180:
                    result.X = this.ActualWidth - point.X;
                    result.Y = this.ActualHeight - point.Y;
                    break;

                case 270:
                    result.X = point.Y;
                    result.Y = this.ActualHeight - point.X;

                    break;
            }

            return result;
        }

        private Point InvertRotateViewPortPoint(Point point)
        {

            Point result = new Point();
            switch (_rotateAngle)
            {
                case 0:
                    result.X = point.X;
                    result.Y = point.Y;
                    break;

                case 90:
                    result.X = point.Y;
                    result.Y = this.ActualHeight - point.X;

                    break;

                case 180:
                    result.X = this.ActualWidth - point.X;
                    result.Y = this.ActualHeight - point.Y;
                    break;

                case 270:
                    result.X = this.ActualWidth - point.Y;
                    result.Y = point.X;

                    break;
            }

            return result;
        }

        private Point RotateImagePoint(Point point)
        {

            Point result = new Point();
            switch (_rotateAngle)
            {
                case 0:
                    result.X = point.X;
                    result.Y = point.Y;
                    break;

                case 90:
                    result.X = this._pictureWidth - point.Y;
                    result.Y = point.X;

                    break;

                case 180:
                    result.X = this._pictureWidth - point.X;
                    result.Y = this._pictureHeight - point.Y;
                    break;

                case 270:
                    result.X = point.Y;
                    result.Y = this._pictureHeight - point.X;

                    break;
            }

            return result;
        }

        private Point InvertRotateImagePoint(Point point)
        {

            Point result = new Point();
            switch (_rotateAngle)
            {
                case 0:
                    result.X = point.X;
                    result.Y = point.Y;
                    break;

                case 90:
                    result.X = point.Y;
                    result.Y = this._pictureWidth - point.X;

                    break;

                case 180:
                    result.X = this._pictureWidth - point.X;
                    result.Y = this._pictureHeight - point.Y;
                    break;

                case 270:
                    result.X = this._pictureHeight - point.Y;
                    result.Y = point.X;

                    break;
            }

            return result;
        }

        private NoteShape GetNoteShape(Drawing drawing, string name)
        {
            string realName = name.Substring(0, name.Length - controlIndex.ToString().Length);


            foreach (NoteShape shape in drawing.Shapes)
            {
                if (shape.Name == realName)
                    return shape;
            }


            throw new InvalidProgramException("Unknown shape");
        }

        void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            NoteText notificationText = (NoteText)GetNoteShape(CurrentDrawing, textBox.Name);
            notificationText.Text = textBox.Text;

            ShowCurrentShapeMover();

            if (NoteDrawChanged != null)
                NoteDrawChanged(this, EventArgs.Empty);
        }

        private void UnselectCurrentElement()
        {
            Shape currentShape = _currentElement as Shape;
            if (currentShape != null)
            {
                //currentShape.StrokeDashArray.Clear();
            }
            else
            {
                TextBox currentTextBox = _currentElement as TextBox;
                if (currentTextBox != null)
                {
                    int index = _currentNotifyElementList.IndexOf(currentTextBox);

                    if (index >= 0)
                        _currentNotifyElementList.RemoveAt(index);

                    if (!string.IsNullOrEmpty(currentTextBox.Text) && !string.IsNullOrWhiteSpace(currentTextBox.Text))
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.Cursor = Cursors.Arrow;
                        textBlock.Text = currentTextBox.Text;
                        textBlock.RenderTransform = currentTextBox.RenderTransform;
                        textBlock.Tag = "TextBlock";
                        textBlock.FontSize = currentTextBox.FontSize;
                        textBlock.Foreground = currentTextBox.Foreground;
                        Canvas.SetLeft(textBlock, Canvas.GetLeft(currentTextBox));
                        Canvas.SetTop(textBlock, Canvas.GetTop(currentTextBox));
                        WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(textBlock, this, (ls, s, e) => ls.currentShape_MouseLeftButtonDown(s, e));

                        if (index > -1)
                            _currentNotifyElementList.Insert(index, textBlock);
                        else
                            _currentNotifyElementList.Add(textBlock);

                        textBlock.Name = currentTextBox.Name;

                        CurrentElement = textBlock;
                    }
                    else
                    {
                        CurrentElement = null;
                    }
                }
            }
        }

        void currentShape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (EditNote && ((NotifyType == MaVuViecClient.NotifyType.None) || (NotifyType == MaVuViecClient.NotifyType.Draw))) //(NotifyType == MaVuViecClient.NotifyType.DrawArrow)
            {
                if (CurrentElement != null)
                {
                    UnselectCurrentElement();
                }

                CurrentElement = (FrameworkElement)sender;
                Shape currentShape = _currentElement as Shape;
                if (currentShape != null)
                {
                    //currentShape.StrokeDashArray.Add(4);
                    //currentShape.StrokeDashArray.Add(1);
                    e.Handled = true;
                }
                else
                {
                    TextBlock currentTextBlock = _currentElement as TextBlock;
                    if (currentTextBlock != null)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Text = currentTextBlock.Text;
                        textBox.Tag = "TextBox";
                        textBox.FontSize = currentTextBlock.FontSize;
                        textBox.Foreground = currentTextBlock.Foreground;
                        textBox.CaretBrush = new SolidColorBrush(((SolidColorBrush)currentTextBlock.Foreground).Color);
                        textBox.AcceptsReturn = true;
                        textBox.RenderTransform = currentTextBlock.RenderTransform;

                        Canvas.SetLeft(textBox, Canvas.GetLeft(currentTextBlock));
                        Canvas.SetTop(textBox, Canvas.GetTop(currentTextBlock));
                        WeakEventHelper.RegisterMouseLeftButtonDown<PictureViewer>(textBox, this, (ls, s, we) => ls.currentShape_MouseLeftButtonDown(s, we));
                        WeakEventHelper.RegisterTextBoxTextChanged<PictureViewer>(textBox, this, (ls, s, we) => ls.textBox_TextChanged(s, we));


                        int index = _currentNotifyElementList.IndexOf(currentTextBlock);
                        _currentNotifyElementList.RemoveAt(index);
                        _currentNotifyElementList.Insert(index, textBox);

                        textBox.Name = currentTextBlock.Name;

                        CurrentElement = textBox;


                        textBox.Focus();
                        textBox.SelectAll();

                        e.Handled = true;
                    }
                }
            }
        }

        private Point GetRelativeViewPortLocation(Point location)
        {
            double scale = 1.0;
            for (int i = RenderLevel - 1; i >= 0; i--)
            {
                scale = scale / 2.0;
            }

            double viewPortLeft = _pictureWidth * ViewPort.X;
            double viewPortTop = _pictureHeight * ViewPort.Y;
            double x = viewPortLeft + location.X / scale;
            double y = viewPortTop + location.Y / scale;

            return new Point(x, y);
        }

        public event EventHandler NoteDrawChanged;
        private DocLink currentLinkAdded;
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (!isUserInteractionEnabled)
                return;

            if (IsSelectedArea && _mouseDownActive)
            {
                this.ReleaseMouseCapture();
                _mouseDownActive = false;
                base.OnMouseLeftButtonUp(e);

                System.Diagnostics.Debug.WriteLine(Canvas.GetLeft(RectSelectedArea).ToString() + " - " + Canvas.GetTop(RectSelectedArea).ToString());
                Rectangles.Add(_rectSelectedArea);

                if (SelectedAreaHandler != null)
                    SelectedAreaHandler(this, EventArgs.Empty);

                return;
            }

            if (AddLinks)
            {
                Point mouseLocation = e.GetPosition(this);
                int rlPixelWidth = _layers[RenderLevel].PixelWidth;
                int rlPixelHeight = _layers[RenderLevel].PixelHeight;

                double viewPortLeft = rlPixelWidth / 2 - _renderCenter.X;
                double viewPortTop = rlPixelHeight / 2 - _renderCenter.Y;

                currentLinkAdded = new DocLink((viewPortLeft + mouseLocation.X) / rlPixelWidth, (viewPortTop + mouseLocation.Y) / rlPixelHeight);
                _linksList.Add(currentLinkAdded);

                UpdateLinks();

                base.OnMouseLeftButtonUp(e);

                if (LinkAdded != null)
                    LinkAdded(this, EventArgs.Empty);

                return;
            }

            if (_shapeHandler1IsInMove || shapeHandler2IsInMove || _shapeMoverIsInMove)
            {
                base.OnMouseLeftButtonUp(e);
                return;
            }

            if (NotifyType != MaVuViecClient.NotifyType.DrawText)
            {
                this.ReleaseMouseCapture();
            }
            if (_mouseDownActive)
            {
                if ((NotifyType != MaVuViecClient.NotifyType.None) && (NotifyType != MaVuViecClient.NotifyType.Draw)) // && (NotifyType != MaVuViecClient.NotifyType.DrawArrow))
                {
                    Drawing drawing = CurrentDrawing;

                    if (NotifyType == MaVuViecClient.NotifyType.Crop)
                    {
                        drawing.Left = (int)Canvas.GetLeft(CurrentNotifyRectangle);
                        drawing.Top = (int)Canvas.GetTop(CurrentNotifyRectangle);
                        drawing.Width = (int)CurrentNotifyRectangle.Width;
                        drawing.Height = (int)CurrentNotifyRectangle.Height;

                        CurrentNotifyRectangle.GuidID = currentNote.GuidId;
                        drawing.Scale = GetCurrentScale();
                        drawing.RenderCenterX = this._renderCenter.X;
                        drawing.RenderCenterY = this._renderCenter.Y;

                        drawing.HasRectangle = true;

                        if (NoteDrawChanged != null)
                            NoteDrawChanged(this, EventArgs.Empty);
                    }
                    else
                    {
                        if (CurrentElement != null)
                        {
                            ShowCurrentShapeHandlers();
                            ShowCurrentShapeMover();
                            ShowShapePinMover();

                            if (NoteDrawChanged != null)
                                NoteDrawChanged(this, EventArgs.Empty);
                        }
                    }
                }

                _mouseDownActive = false;
            }
            base.OnMouseLeftButtonUp(e);
        }

        public void CancelAddLink()
        {
            if (currentLinkAdded != null)
            {
                _linksList.Remove(currentLinkAdded);
                currentLinkAdded.Dispose();
                currentLinkAdded = null;
                UpdateLinks();
            }
        }

        public void ConfirmAddLink(Guid planGuidID)
        {
            if (currentLinkAdded != null)
            {
                currentLinkAdded.PlanGuidID = planGuidID;
                currentLinkAdded = null;
                UpdateLinks();
            }
        }

        public void UpdateNoteScaleAndCenter()
        {
            Drawing drawing = CurrentDrawing;
            if (drawing != null)
            {
                drawing.Scale = GetCurrentScale();
                Point newNoteRenderCenter = InvertRotateViewPortPoint(new Point(this._renderCenter.X, this._renderCenter.Y));
                drawing.RenderCenterX = newNoteRenderCenter.X;
                drawing.RenderCenterY = newNoteRenderCenter.Y;
                drawing.HasRectangle = (_currentNotifyRectangle != null);
            }

        }

        private void UpdateDisabledControlsList(DependencyObject element, List<Control> disabledControls)
        {
            Control control = element as Control;
            if ((control != null) && !control.IsEnabled)
                disabledControls.Add(control);

            int childCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCount; i++)
                UpdateDisabledControlsList(VisualTreeHelper.GetChild(element, i), disabledControls);
        }

        //void notifyWindow_Closed(object sender, EventArgs e)
        //{
        //    Notify = false;

        //    if (notifyWindow.DialogResult != true)
        //    {
        //        notificationsList.Remove(CurrentNote);
        //        CurrentNote = null;
        //    }
        //    notifyWindow.Closed -= new EventHandler(notifyWindow_Closed);
        //    notifyWindow = null;
        //}

        Point mouseCurrentLocation = new Point(0, 0);
        protected override void OnMouseMove(MouseEventArgs e)
        {
            IsMouseIn = true;

            if (!isUserInteractionEnabled)
                return;

            if (IsSelectedArea && _mouseDownActive)
            {
                Point mouseLocation = e.GetPosition(this);

                Point mouseDownRelativeInitialLocation = InvertRotateImagePoint(GetRelativeViewPortLocation(_mouseDownInitialLocation));
                Point mouseDownRelativeLocation = InvertRotateImagePoint(GetRelativeViewPortLocation(mouseLocation));

                double deltaX = mouseDownRelativeLocation.X - mouseDownRelativeInitialLocation.X;
                double deltaY = mouseDownRelativeLocation.Y - mouseDownRelativeInitialLocation.Y;

                /*if (deltaX > 0)                
                    Canvas.SetLeft(RectSelectedArea, mouseDownRelativeInitialLocation.X);                
                else                
                    Canvas.SetLeft(RectSelectedArea, mouseDownRelativeLocation.X);                
                */

                if (deltaY > 0)
                {
                    if (IsOverlapsRectangles(Canvas.GetTop(RectSelectedArea), deltaY))
                    {
                        base.OnMouseMove(e);
                        return;
                    }
                }
                else
                {
                    if (IsOverlapsRectangles(mouseDownRelativeLocation.Y, Math.Abs(deltaY)))
                    {
                        base.OnMouseMove(e);
                        return;
                    }
                }

                if (deltaY > 0)
                    Canvas.SetTop(RectSelectedArea, mouseDownRelativeInitialLocation.Y);
                else
                    Canvas.SetTop(RectSelectedArea, mouseDownRelativeLocation.Y);

                //RectSelectedArea.Width = Math.Abs(deltaX);
                RectSelectedArea.Height = Math.Abs(deltaY);

                mouseCurrentLocation = e.GetPosition(this);
                DisplayCursor(Cursor == Cursors.None, mouseCurrentLocation);

                base.OnMouseMove(e);

                return;
            }

            if (_shapeHandler1IsInMove || shapeHandler2IsInMove || _shapeMoverIsInMove)
                return;

            if (_mouseDownActive)
            {
                Point mouseLocation = e.GetPosition(this);

                if ((NotifyType != NotifyType.None) && (NotifyType != NotifyType.Draw)) // && (NotifyType != MaVuViecClient.NotifyType.DrawArrow))
                {
                    Point mouseDownRelativeInitialLocation = InvertRotateImagePoint(GetRelativeViewPortLocation(_mouseDownInitialLocation));
                    Point mouseDownRelativeLocation = InvertRotateImagePoint(GetRelativeViewPortLocation(mouseLocation));

                    double deltaX = mouseDownRelativeLocation.X - mouseDownRelativeInitialLocation.X;
                    double deltaY = mouseDownRelativeLocation.Y - mouseDownRelativeInitialLocation.Y;
                    if (NotifyType == NotifyType.Crop)
                    {
                        if (deltaX > 0)
                        {
                            Canvas.SetLeft(CurrentNotifyRectangle, mouseDownRelativeInitialLocation.X);

                        }
                        else
                        {
                            Canvas.SetLeft(CurrentNotifyRectangle, mouseDownRelativeLocation.X);
                        }

                        if (deltaY > 0)
                        {
                            Canvas.SetTop(CurrentNotifyRectangle, mouseDownRelativeInitialLocation.Y);

                        }
                        else
                        {
                            Canvas.SetTop(CurrentNotifyRectangle, mouseDownRelativeLocation.Y);
                        }

                        CurrentNotifyRectangle.Width = Math.Abs(deltaX);
                        CurrentNotifyRectangle.Height = Math.Abs(deltaY);


                    }
                    else
                    {
                        NoteShape notificationShape = GetNoteShape(CurrentDrawing, CurrentElement.Name);

                        if ((NotifyType == NotifyType.DrawRectangle) ||
                            (NotifyType == NotifyType.DrawElipse) ||
                            (NotifyType == NotifyType.DrawCloud))
                        {
                            NoteFlatShape notificationFlatShape = (NoteFlatShape)notificationShape;
                            if (deltaX > 0)
                            {
                                Canvas.SetLeft(CurrentElement, mouseDownRelativeInitialLocation.X);
                                notificationFlatShape.X = mouseDownRelativeInitialLocation.X;

                            }
                            else
                            {
                                Canvas.SetLeft(CurrentElement, mouseDownRelativeLocation.X);
                                notificationFlatShape.X = mouseDownRelativeLocation.X;
                            }


                            if (deltaY > 0)
                            {
                                Canvas.SetTop(CurrentElement, mouseDownRelativeInitialLocation.Y);
                                notificationFlatShape.Y = mouseDownRelativeInitialLocation.Y;
                            }
                            else
                            {
                                Canvas.SetTop(CurrentElement, mouseDownRelativeLocation.Y);
                                notificationFlatShape.Y = mouseDownRelativeLocation.Y;
                            }

                            CurrentElement.Width = Math.Abs(deltaX);
                            notificationFlatShape.Width = Math.Abs(deltaX);
                            CurrentElement.Height = Math.Abs(deltaY);
                            notificationFlatShape.Height = Math.Abs(deltaY);
                        }
                        else if (NotifyType == NotifyType.DrawLine)
                        {
                            Line line = (Line)CurrentElement;
                            line.X2 = mouseDownRelativeLocation.X;
                            line.Y2 = mouseDownRelativeLocation.Y;

                            NoteLine notificationLine = (NoteLine)notificationShape;
                            notificationLine.X2 = line.X2;
                            notificationLine.Y2 = line.Y2;
                        }
                        else if (NotifyType == NotifyType.DrawFree)
                        {
                            NotePolyline notificationPolyline = (NotePolyline)notificationShape;

                            Polyline polyline = (Polyline)CurrentElement;
                            //int currentPointIndex = polyline.Points.IndexOf(currentPolyLinePoint);
                            //polyline.Points.RemoveAt(currentPointIndex);
                            //notificationPolyline.Points.RemoveAt(currentPointIndex);


                            _currentPolyLinePoint.X = mouseDownRelativeLocation.X;
                            _currentPolyLinePoint.Y = mouseDownRelativeLocation.Y;

                            polyline.Points.Add(_currentPolyLinePoint);
                            notificationPolyline.Points.Add(_currentPolyLinePoint);

                        }
                        else if (NotifyType == MaVuViecClient.NotifyType.DrawPolyLine)
                        {
                            NotePolyline notificationPolyline = (NotePolyline)notificationShape;

                            Polyline polyline = (Polyline)CurrentElement;
                            polyline.Points.RemoveAt(polyline.Points.Count - 1);
                            notificationPolyline.Points.RemoveAt(notificationPolyline.Points.Count - 1);

                            Point currentPolyLinePoint = new Point(mouseDownRelativeLocation.X, mouseDownRelativeLocation.Y);

                            polyline.Points.Add(currentPolyLinePoint);
                            notificationPolyline.Points.Add(currentPolyLinePoint);

                        }

                        ShowCurrentShapeHandlers();
                        ShowCurrentShapeMover();
                        ShowShapePinMover();

                    }

                    if (mouseLocation.X < 0)
                    {
                        _renderCenter.X -= mouseLocation.X;
                        _mouseDownInitialRenderCenter.X += mouseLocation.X;
                        _mouseDownInitialLocation.X -= mouseLocation.X;
                        if (NotifyType == NotifyType.Crop)
                        {
                            CurrentNotifyRectangle.Width += mouseLocation.X;
                        }

                        UpdateTiles(this.ActualWidth, this.ActualHeight);
                    }
                    else if (mouseLocation.X > this.ActualWidth)
                    {
                        double extraX = mouseLocation.X - this.ActualWidth;
                        _renderCenter.X -= extraX;
                        _mouseDownInitialRenderCenter.X += extraX;
                        _mouseDownInitialLocation.X -= extraX;
                        if (NotifyType == NotifyType.Crop)
                        {
                            CurrentNotifyRectangle.Width += extraX;
                        }

                        UpdateTiles(this.ActualWidth, this.ActualHeight);
                    }

                    if (mouseLocation.Y < 0)
                    {
                        _renderCenter.Y -= mouseLocation.Y;
                        _mouseDownInitialRenderCenter.Y += mouseLocation.Y;
                        _mouseDownInitialLocation.Y -= mouseLocation.Y;
                        if (NotifyType == NotifyType.Crop)
                        {
                            CurrentNotifyRectangle.Width += mouseLocation.Y;
                        }

                        UpdateTiles(this.ActualWidth, this.ActualHeight);
                    }
                    else if (mouseLocation.Y > this.ActualHeight)
                    {
                        double extraY = mouseLocation.Y - this.ActualHeight;
                        _renderCenter.Y -= extraY;
                        _mouseDownInitialRenderCenter.Y += extraY;
                        _mouseDownInitialLocation.Y -= extraY;
                        if (NotifyType == NotifyType.Crop)
                        {
                            CurrentNotifyRectangle.Width += extraY;
                        }

                        UpdateTiles(this.ActualWidth, this.ActualHeight);
                    }
                }
                else
                {

                    _renderCenter = new Point(_mouseDownInitialRenderCenter.X + mouseLocation.X - _mouseDownInitialLocation.X,
                                                _mouseDownInitialRenderCenter.Y + mouseLocation.Y - _mouseDownInitialLocation.Y);
                    this.Dispatcher.BeginInvoke(() => UpdateTiles(this.ActualWidth, this.ActualHeight));
                }

                //UpdateTiles(this.ActualWidth, this.ActualHeight);
            }

            mouseCurrentLocation = e.GetPosition(this);
            DisplayCursor(Cursor == Cursors.None, mouseCurrentLocation);

            base.OnMouseMove(e);
        }


        public bool IsMouseIn
        {
            get { return _isMouseIn; }
            set
            {
                if (_isMouseIn != value)
                {
                    _isMouseIn = value;
                    if (_isMouseIn)
                    {
                        if (MouseIsIn != null)
                            MouseIsIn(this, EventArgs.Empty);
                    }
                }
            }
        }


        protected override void OnMouseEnter(MouseEventArgs e)
        {
            IsMouseIn = true;
            DisplayCursor(Cursor == Cursors.None, e.GetPosition(this));
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            IsMouseIn = false;
            DisplayCursor(false);
            base.OnMouseLeave(e);
        }

        //public event EventHandler MouseWheelHelperChange;
        void mouseWheelHelper_Moved(object sender, MouseWheelEventArgs e)
        {
            if (!isUserInteractionEnabled)
                return;

            if (!IsMouseIn)
                return;

            if (e.Delta > 0 && RenderLevel > 0) // zoom-in
            {
                ZoomIn(mouseCurrentLocation);
            }
            else if (e.Delta < 0) // zoom-out
            {
                ZoomOut(mouseCurrentLocation);
            }
            //if (MouseWheelHelperChange != null)
            //    MouseWheelHelperChange(this, EventArgs.Empty);
        }

        public void ZoomIn(Point zoomPoint)
        {
            if (RenderLevel > 0)
            {
                HideTiles();
                RenderLevel--;

                //place the center at a location so that the pixel pointed by the mouse cursor do not move after the zoom
                double deltaX = (zoomPoint.X - (this.ActualWidth / 2));
                double deltaY = (zoomPoint.Y - (this.ActualHeight / 2));

                _renderCenter.X -= deltaX / 2.0;
                _renderCenter.Y -= deltaY / 2.0;

                //Keep center at center after zoom
                _renderCenter.X -= ActualWidth / 2.0;
                _renderCenter.X *= 2;
                _renderCenter.X += ActualWidth / 2.0;

                _renderCenter.Y -= ActualHeight / 2.0;
                _renderCenter.Y *= 2;
                _renderCenter.Y += ActualHeight / 2.0;


                UpdateTiles(this.ActualWidth, this.ActualHeight);
            }
        }

        public void RelativeZoomIn(Point relativeZoomPoint)
        {
            //Get zoomPoint current coordinates
            Point zoomPoint = new Point();
            zoomPoint.X = relativeZoomPoint.X * this.ActualWidth;
            zoomPoint.Y = relativeZoomPoint.Y * this.ActualHeight;


            ZoomIn(zoomPoint);

        }

        public void ZoomOut(Point zoomPoint)
        {
            if (RenderLevel < _layers.Count - 1)
            {
                HideTiles();
                RenderLevel++;

                //place the center at a location so that the pixel pointed by the mouse cursor do not move after the zoom
                double deltaX = (zoomPoint.X - (this.ActualWidth / 2));
                double deltaY = (zoomPoint.Y - (this.ActualHeight / 2));

                _renderCenter.X += deltaX;
                _renderCenter.Y += deltaY;

                //Keep center at center after zoom
                _renderCenter.X -= ActualWidth / 2.0;
                _renderCenter.X /= 2;
                _renderCenter.X += ActualWidth / 2.0;

                _renderCenter.Y -= ActualHeight / 2.0;
                _renderCenter.Y /= 2;
                _renderCenter.Y += ActualHeight / 2.0;

                UpdateTiles(this.ActualWidth, this.ActualHeight);
            }
        }

        public void RelativeZoomOut(Point relativeZoomPoint)
        {
            //Get zoomPoint current coordinates
            Point zoomPoint = new Point();
            zoomPoint.X = relativeZoomPoint.X * this.ActualWidth;
            zoomPoint.Y = relativeZoomPoint.Y * this.ActualHeight;


            ZoomOut(zoomPoint);
        }

        public void Zoom(double scale, double renderCenterX, double renderCenterY)
        {
            RenderLevel = GetRenderLevel(scale);
            _renderCenter.X = renderCenterX;
            _renderCenter.Y = renderCenterY;
            UpdateTiles(this.ActualWidth, this.ActualHeight);
        }


        public void ZoomByProgress(double scale)
        {
            int newRenderLevel = GetRenderLevel(scale);
            if (RenderLevel == newRenderLevel)
                return;

            if (RenderLevel < newRenderLevel)
            {
                ScaleOut();
            }
            else
            {
                ScaleIn();
            }
            //ScaleIn();
            Zoom(scale, _renderCenter.X, _renderCenter.Y);
        }

        void ScaleIn()
        {
            _renderCenter.X -= ActualWidth / 2.0;
            _renderCenter.X *= 2;
            _renderCenter.X += ActualWidth / 2.0;

            _renderCenter.Y -= ActualHeight / 2.0;
            _renderCenter.Y *= 2;
            _renderCenter.Y += ActualHeight / 2.0;

        }

        void ScaleOut()
        {
            _renderCenter.X -= ActualWidth / 2.0;
            _renderCenter.X /= 2;
            _renderCenter.X += ActualWidth / 2.0;

            _renderCenter.Y -= ActualHeight / 2.0;
            _renderCenter.Y /= 2;
            _renderCenter.Y += ActualHeight / 2.0;
        }


        private class Layer
        {
            public readonly int Level, PixelWidth, PixelHeight, TileWidth, TileHeight;
            public readonly Tile[] Tiles;
            public readonly bool IsBigThumb;

            public Layer(int level, int pixelWidth, int pixelHeight, int tileSize, bool isBigThumb)
            {
                Level = level;
                PixelWidth = pixelWidth;
                PixelHeight = pixelHeight;

                IsBigThumb = isBigThumb;

                if (isBigThumb)
                {
                    TileWidth = 1;
                    TileHeight = 1;
                }
                else
                {
                    TileWidth = (int)Math.Ceiling((double)pixelWidth / tileSize);
                    TileHeight = (int)Math.Ceiling((double)pixelHeight / tileSize);
                }
                Tiles = new Tile[TileWidth * TileHeight];

            }
        }

        private class Tile
        {
            public Tile(Image image, int layerLevel, int imageIndex)
            {
                Image = image;
                LayerLevel = layerLevel;
                ImageIndex = imageIndex;
            }

            public Image Image
            {
                get;
                set;
            }

            public int LayerLevel
            {
                get;
                set;
            }

            public int ImageIndex
            {
                get;
                set;
            }

            public string TileName
            {
                get;
                set;
            }

            public string LayerName
            {
                get;
                set;
            }
        }

        private class HightlightCriteria
        {
            public Rect OriginalRect { get; set; }
            public Rectangle Rectangle { get; set; }
        }

        public bool SetViewPortLocationIsEnabled
        {
            get;
            set;
        }

        public void SetViewPortLocation(Point viewPortLocation)
        {
            if (!SetViewPortLocationIsEnabled)
                return;

            if (!isAPlanShown)
                return;
            if (_layers.Count - 1 < RenderLevel)
                return;

            int rlPixelWidth = _layers[RenderLevel].PixelWidth;
            int rlPixelHeight = _layers[RenderLevel].PixelHeight;

            _renderCenter.X = rlPixelWidth / 2.0 - viewPortLocation.X * rlPixelWidth;
            _renderCenter.Y = rlPixelHeight / 2.0 - viewPortLocation.Y * rlPixelHeight;

            UpdateTiles(this.ActualWidth, this.ActualHeight);
        }

        private Rect viewPort;
        public Rect ViewPort
        {
            get { return viewPort; }
            private set
            {
                if (viewPort != value)
                {
                    viewPort = value;
                    OnViewPortChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler ViewPortChanged;
        protected void OnViewPortChanged(EventArgs e)
        {
            if (ViewPortChanged != null)
                ViewPortChanged(this, e);
        }

        private GridOptions gridOptions;
        public GridOptions GridOptions
        {
            get { return gridOptions; }
            set
            {
                if (gridOptions != value)
                {
                    gridOptions = value;
                    this.gridOptionsBtn.IsEnabled = (gridOptions != null) && isAPlanShown && !editNote;
                }
            }
        }

        public void CheckGridOptions()
        {
            if (this.gridOptionsBtn != null)
                this.gridOptionsBtn.IsChecked = true;
        }

        public void UncheckGridOptions()
        {
            if (this.gridOptionsBtn != null)
                this.gridOptionsBtn.IsChecked = false;
        }

        private bool isInClickOrigine;
        public void InitializeClickOrigine()
        {
            isInClickOrigine = true;
            this.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        public void CancelInitializeClickOrigine()
        {
            isInClickOrigine = false;
            this.Cursor = System.Windows.Input.Cursors.Hand;
        }

        public event EventHandler LinkAdded;
        private bool addLinks;
        public bool AddLinks
        {
            get { return addLinks; }
            set
            {
                if (value != addLinks)
                {
                    addLinks = value;
                    OnAddLinksChanged();
                }
            }
        }

        //public event EventHandler LinkAdded;
        private bool isSelectedAred;
        public bool IsSelectedArea
        {
            get { return isSelectedAred; }
            set
            {
                if (value != isSelectedAred)
                {
                    isSelectedAred = value;
                    OnAddSelectedAreChanged();
                }
            }
        }

        protected void OnAddLinksChanged()
        {
            if (addLinks)
                this.Cursor = Cursors.Arrow;
            else
                this.Cursor = Cursors.Hand;
        }

        protected void OnAddSelectedAreChanged()
        {
            if (IsSelectedArea)
                this.Cursor = Cursors.Arrow;
            else
                this.Cursor = Cursors.Hand;
        }

        public void UpdateRotateAngle(int newRotateAngle)
        {
            int delta = _rotateAngle - newRotateAngle;
            _rotateAngle = newRotateAngle;

            switch (delta)
            {
                case 0:
                    break;

                case -270:
                case 90: //RotateLeft
                    double tempRenderCenterX = _renderCenter.X;
                    _renderCenter.X = (this.ActualWidth / 2) - (this.ActualHeight / 2) + _renderCenter.Y;
                    _renderCenter.Y = (this.ActualWidth / 2) + (this.ActualHeight / 2) - tempRenderCenterX;

                    _Rotate();
                    break;

                case 270:
                case -90: //RotateRight
                    double tempRenderCenterX2 = _renderCenter.X;
                    _renderCenter.X = (this.ActualHeight / 2) - _renderCenter.Y + (this.ActualWidth / 2);
                    _renderCenter.Y = tempRenderCenterX2 - (this.ActualWidth / 2) + (this.ActualHeight / 2);

                    _Rotate();
                    break;

                case -180:
                case 180:
                    double tempRenderCenterX3 = _renderCenter.X;
                    _renderCenter.X = (this.ActualWidth / 2) - (this.ActualHeight / 2) + _renderCenter.Y;
                    _renderCenter.Y = (this.ActualWidth / 2) + (this.ActualHeight / 2) - tempRenderCenterX3;
                    _Rotate();

                    tempRenderCenterX3 = _renderCenter.X;
                    _renderCenter.X = (this.ActualWidth / 2) - (this.ActualHeight / 2) + _renderCenter.Y;
                    _renderCenter.Y = (this.ActualWidth / 2) + (this.ActualHeight / 2) - tempRenderCenterX3;

                    _Rotate();
                    break;
            }

        }

        public void RotateLeft()
        {
            _rotateAngle -= 90;
            if (_rotateAngle == -90)
                _rotateAngle = 270;

            double tempRenderCenterX = _renderCenter.X;
            _renderCenter.X = (ActualWidth / 2) - (ActualHeight / 2) + _renderCenter.Y;
            _renderCenter.Y = (ActualWidth / 2) + (ActualHeight / 2) - tempRenderCenterX;

            _Rotate();
        }

        public void RotateRight()
        {
            _rotateAngle += 90;
            if (_rotateAngle == 360)
                _rotateAngle = 0;

            double tempRenderCenterX = _renderCenter.X;
            _renderCenter.X = (ActualHeight / 2) - _renderCenter.Y + (ActualWidth / 2);
            _renderCenter.Y = tempRenderCenterX - (ActualWidth / 2) + (ActualHeight / 2);


            _Rotate();
        }

        private void _Rotate()
        {
            if (PlanId < 0)
                return;

            int iniPlanID = PlanId;
            int iniVersionNbr = VersionNbr;
            Guid iniFolderGuid = FolderGuid;
            int iniTileSize = _tileSize;
            int iniRotateAngle = _rotateAngle;
            Cursor iniCursor = Cursor;

            GridOptions iniGridOptions = gridOptions;

            string iniTilesPath = _tilesPath;

            int iniPictureWidth = _pictureHeight;
            int iniPictureHeight = _pictureWidth;
            int iniNbrOfZoomLevels = _layers.Count - 1;
            int iniCurrentBigThumbWidth = _layers[iniNbrOfZoomLevels].PixelHeight;
            int iniCurrentBigThumbHeight = _layers[iniNbrOfZoomLevels].PixelWidth;

            int iniRenderLevel = _renderLevel;
            Point iniRenderCenter = _renderCenter;

            //Clear();
            DisposeTiles();
            //DisposeNotes();
            UpdateTiles(this.ActualWidth, this.ActualHeight);
            UpdateLayout();

            PlanId = iniPlanID;
            VersionNbr = iniVersionNbr;
            FolderGuid = iniFolderGuid;
            _tileSize = iniTileSize;
            _rotateAngle = iniRotateAngle;

            gridOptions = iniGridOptions;


            //this.gridOptions.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(gridOptions_PropertyChanged);
            isAPlanShown = true;
            Cursor = iniCursor;

            _tilesPath = iniTilesPath;

            _pictureWidth = iniPictureWidth;
            _pictureHeight = iniPictureHeight;

            int nbrOfZoomLevels = iniNbrOfZoomLevels;

            int currentBigThumbWidth = iniCurrentBigThumbWidth;
            int currentBigThumbHeight = iniCurrentBigThumbHeight;

            _layers.Clear();
            int currentWidth = _pictureWidth;
            int currentHeight = _pictureHeight;
            int maxLevel = -1;
            for (int level = 0; level < nbrOfZoomLevels; level++)
            {
                maxLevel++;
                _layers.Add(new Layer(maxLevel, currentWidth, currentHeight, _tileSize, false));
                currentWidth = (int)Math.Ceiling((double)currentWidth / 2.0);
                currentHeight = (int)Math.Ceiling((double)currentHeight / 2.0);

            }

            maxLevel++;
            _layers.Add(new Layer(maxLevel, currentBigThumbWidth, currentBigThumbHeight, _tileSize, true));

            _renderLevel = iniRenderLevel;
            _renderCenter = iniRenderCenter;

            UpdateTiles(this.ActualWidth, this.ActualHeight);
            InitializeGrid();
            UpdateGrid();
            UpdateDrawingCanvas();
            RefreshTransformPin();
        }

        private void RefreshTransformPin()
        {
            if (this.ActualHeight == 0 || this.ActualHeight == 0)
                return;
            ItemPin itPin;
            if (_currentNotifyElementList != null && _currentNotifyElementList.Count > 0)
            {
                foreach (FrameworkElement noteShape in _currentNotifyElementList)
                {
                    itPin = noteShape as ItemPin;
                    if (itPin != null)
                    {
                        currentItemPin = itPin;
                        CurrentElement = currentItemPin;

                        RotateTransform currentItemPinTransform = new RotateTransform();
                        currentItemPinTransform.CenterX = 0;
                        currentItemPinTransform.CenterY = 0;
                        currentItemPinTransform.Angle = 360 - _rotateAngle;
                        if (currentItemPin != null)
                        {
                            NoteShape notificationShape = null;
                            if (CurrentDrawing != null && CurrentDrawing.Shapes.Count > 0)
                                notificationShape = GetNoteShape(CurrentDrawing, currentItemPin.Name);
                            NotePin pin = notificationShape as NotePin;
                            if (pin == null)
                                return;
                            currentItemPin.RenderTransform = currentItemPinTransform;
                            if (pin != null)
                            {
                                RotatePin(new Point(pin.X, pin.Y));
                                ShowShapePinMover();
                            }
                        }
                    }
                }
            }


        }

        #region Pin image
        private Image _pinImage;
        private Point _pinOriginPoint;
        public void LocatePin(Point point, bool canShow = true, bool canCenterView = false)
        {
            // keep origin point for during view/zoom
            _pinOriginPoint = point;
            // able to show pin
            CanShowPin = canShow;
            // able to center pin
            if (CanShowPin && canCenterView)
            {
                var imgW = _layers[this.RenderLevel].PixelWidth;
                var imgH = _layers[this.RenderLevel].PixelHeight;

                var xcenter = (imgW / 2) - point.X * GetCurrentScale() + this.ActualWidth / 2;
                var ycenter = (imgH / 2) - point.Y * GetCurrentScale() + this.ActualHeight / 2;
                RenderCenter = new Point(xcenter, ycenter);
            }
        }

        private bool _canShowPin = false;
        public bool CanShowPin
        {
            get { return _canShowPin; }
            set
            {
                if (_canShowPin != value)
                {
                    _canShowPin = value;
                    _pinImage.Visibility = _canShowPin ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


        public void ShowMessageNoAccessPlan()
        {
            ALabel lbl = this.GetTemplateChild("TitleLabel") as ALabel;
            if (lbl != null)
                lbl.Visibility = System.Windows.Visibility.Visible;
        }


        private void HiddenMessageNoAccessPlan()
        {
            ALabel lbl = this.GetTemplateChild("TitleLabel") as ALabel;
            if (lbl != null)
                lbl.Visibility = Visibility.Collapsed;
        }

        //Case Empty Plan (Plan.Id <= 0)
        public void HiddenMessageSearchIndex(bool bVal)
        {
            ALabel lbl = this.GetTemplateChild("lblIndexStatus") as ALabel;
            if (lbl != null)
                lbl.Visibility = bVal ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        public bool IsPlanInvisible
        {
            get;
            set;
        }



        public void CenterDocumentIfNoDrawings()
        {
            if ((CurrentDrawing == null) || (CurrentDrawing.Shapes.Count == 0))
            {
                _renderCenter.X = this.ActualWidth / 2;
                _renderCenter.Y = this.ActualHeight / 2;
                UpdateTiles(this.ActualWidth, this.ActualHeight);

            }
        }

        public PlanIndexStatus SearchIndexStatus
        {
            set
            {
                ALabel lbl = this.GetTemplateChild("lblIndexStatus") as ALabel;

                if (lbl != null)
                {
                    if (value != PlanIndexStatus.FullIndexed)
                        lbl.Content = Global.Instance.LanguageDictionary["PlanIndexStatus_" + value.ToString()];
                    else
                        lbl.Content = string.Empty;
                }
            }
        }
    }
}
