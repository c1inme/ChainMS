using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace ControlLibrary
{
    public class PicturePinViewer : Control
    {
        #region Dependency Properties
        public static readonly DependencyProperty PlanProperty =
            DependencyProperty.Register("Plan", typeof(Plan), typeof(PicturePinViewer), null);

        public static readonly DependencyProperty CommentsProperty =
            DependencyProperty.Register("Comments", typeof(ObservableCollection<HandoverComment>), typeof(PicturePinViewer), null);

        public static readonly DependencyProperty VersionProperty =
            DependencyProperty.Register("Version", typeof(int), typeof(PicturePinViewer), null);

        public static readonly DependencyProperty CanZoomProperty =
            DependencyProperty.Register("CanZoom", typeof(bool), typeof(PicturePinViewer), null);

        public static readonly DependencyProperty CanShowLabelProperty =
            DependencyProperty.Register("CanShowLabel", typeof(bool), typeof(PicturePinViewer), null);

        public static readonly DependencyProperty RenderLevelProperty =
            DependencyProperty.Register("RenderLevel", typeof(int), typeof(PicturePinViewer), null);
        #endregion

        #region Controls template
        private Grid _layoutRoot;
        private Canvas _imagesCanvas;
        private Canvas _pinsCanvas;
        private RectangleGeometry _clipRect;
        #endregion

        #region Public Properties
        public Plan Plan
        {
            get { return (Plan)GetValue(PlanProperty); }
            private set { SetValue(PlanProperty, value); }
        }

        //public ObservableCollection<HandoverComment> Comments
        //{
        //    get { return (ObservableCollection<HandoverComment>)GetValue(CommentsProperty); }
        //    private set { SetValue(CommentsProperty, value); }
        //}

        public int Version
        {
            get { return (int)GetValue(VersionProperty); }
            set { SetValue(VersionProperty, value); }
        }

        public bool CanZoom
        {
            get { return (bool)GetValue(CanZoomProperty); }
            set { SetValue(CanZoomProperty, value); }
        }

        public bool CanShowLabel { get { return (bool)GetValue(CanShowLabelProperty); } set { SetValue(CanShowLabelProperty, value); } }

        public int RenderLevel { get { return (int)GetValue(RenderLevelProperty); } set { SetValue(RenderLevelProperty, value); } }
        #endregion

        public PicturePinViewer() { 
            DefaultStyleKey = typeof(PicturePinViewer);
            SimpleWeakEventPattern.ObjectTracker.Track(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            #region Load controls template
            _layoutRoot = this.GetTemplateChild("LayoutRoot") as Grid;

            // canvas image template
            _imagesCanvas = this.GetTemplateChild("ImagesCanvas") as Canvas;

            // Pin template
            _pinsCanvas = this.GetTemplateChild("PinsCanvas") as Canvas;

            // clip template
            _clipRect = this.GetTemplateChild("ClipRect") as RectangleGeometry;
            #endregion

            //DrawComments(new Size(this.ActualWidth, this.ActualHeight));            
            
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Plan == null)
                return base.MeasureOverride(availableSize);
           
            Size result = new Size(availableSize.Width, availableSize.Height);
            if (!CanZoom)
            {
                if ((Plan.RotateAngle == 90) || (Plan.RotateAngle == 270) || (Plan.RotateAngle == -90) || (Plan.RotateAngle == -270))
                {
                    result = new Size(availableSize.Height, availableSize.Width);
                    double suggestedHeight = Plan.Width * availableSize.Height / Plan.Height;
                    if (suggestedHeight > availableSize.Height)
                    {
                        result.Width = Plan.Height * availableSize.Width / Plan.Width;
                    }
                    else
                    {
                        result.Height = suggestedHeight;
                    }
                }
                else
                {
                    double suggestedHeight = Plan.Height * availableSize.Width / Plan.Width;
                    if (suggestedHeight > availableSize.Height)
                    {
                        result.Width = Plan.Width * availableSize.Height / Plan.Height;
                    }
                    else
                    {
                        result.Height = suggestedHeight;
                    }
                }
            }
            else
            {                
                result.Width = Math.Min(availableSize.Width, availableSize.Height);
                result.Height = result.Width;
                
            }




            DrawComments(result);
            return result;
        }

        //protected override Size ArrangeOverride(Size finalSize)
        //{
        //    DrawComments(finalSize);
        //    return base.ArrangeOverride(finalSize);
        //}

        
        //protected override Size ArrangeOverride(Size finalSize)
        //{
        //    DrawComments(finalSize);
        //    return finalSize;
        //}

        private void DrawComments(Size finalSize)
        {
            if (Plan == null || Comments == null || Comments.Count() == 0) return;

            // default, attempt to get last version of Plan needed
            // TODO: wip
            //      base on VersionGuidID of HandoverPlans to attempts last version.
            var lastVersion = new { Plan.Height, Plan.Width, Plan.TileSize, Plan.TilesPath, Plan.ZoomLevelNumber, Plan.RotateAngle, Plan.BigThumbWidth, Plan.BigThumbHeight };
            //if (Version > 0)
            //{
            //    // specify version
            //lastVersion = Plan.Versions.Where(v => v.VersionNbr == Version)
            //                      .Select(v => new { v.Height, v.Width, v.TileSize, v.TilesPath })
            //                      .FirstOrDefault();
            //}
            if (lastVersion != null)
            {
                _imagesCanvas.Children.Clear();
                _pinsCanvas.Children.Clear();
                    

                double realWidth = finalSize.Width;
                double realHeight = finalSize.Height;
                double actualWidth = finalSize.Width;
                double actualHeight = finalSize.Height;

                #region Aspect ratio
                //if (double.IsNaN(this.Width) && double.IsNaN(this.Height))
                //{
                //    this.Width = lastVersion.BigThumbWidth;
                //    this.Height = lastVersion.BigThumbHeight;
                //}
                //else
                //{
                    //double fullPlanWithCommentImageHeight = lastVersion.Height * actualWidth / lastVersion.Width; //this.Height;
                    //double fullPlanWithCommentImageWidth = actualWidth; //plan.Width * 300 / plan.Height;
                    ////this.Height = fullPlanWithCommentImageHeight;
                    //this.Width = fullPlanWithCommentImageWidth;
                //}
                #endregion

                if (CanZoom) // zoom depending to plan info
                {
                    //this.Height = actualHeight;
                    //this.Width = actualWidth;
                    #region Zoom comment
                    // base on first comment to zoom, otherwise ignoring
                    HandoverComment comment = Comments.FirstOrDefault();

                    // offset to draw first tile with good Pin position
                    var offsetX = 0.0;
                    var offsetY = 0.0;

                    //Zoom on the location of the comment
                    //-----------------------------------
                    //We place te location comment at the center of the Zoom frame (ZommPlanWidthCommentCanvas)
                    RenderLevel = Math.Min(RenderLevel, lastVersion.ZoomLevelNumber - 1);
                    int powLvl = (int)Math.Pow(2, RenderLevel);

                    double commentLocationX = comment.Location.X / powLvl;
                    double commentLocationY = comment.Location.Y / powLvl;
                    int fullZoomWidth = lastVersion.Width / powLvl;
                    int fullZoomHeight = lastVersion.Height / powLvl;

                    //string tilesPath = Global.Instance.RootUrl + lastVersion.TilesPath;
                    //double tileSize = lastVersion.TileSize;
                    //double zoomFrameWidth = actualWidth;
                    //double zoomFrameHeight = actualHeight;
                    string tilesPath = Global.Instance.RootUrl + lastVersion.TilesPath;
                    int tileSize = lastVersion.TileSize;
                    //int fullZoomWidth = lastVersion.Width;
                    //int fullZoomHeight = lastVersion.Height;
                    double zoomFrameWidth = actualWidth;
                    double zoomFrameHeight = actualHeight;

                    double zoomFrameLeft = 0;
                    if (commentLocationX + zoomFrameWidth / 2 < fullZoomWidth)
                        zoomFrameLeft = Math.Max(0, commentLocationX - zoomFrameWidth / 2);
                    else
                        zoomFrameLeft = fullZoomWidth - zoomFrameWidth;


                    double zoomFrameTop = 0;
                    if (commentLocationY + zoomFrameHeight / 2 < fullZoomHeight)
                        zoomFrameTop = Math.Max(0, commentLocationY - zoomFrameHeight / 2);
                    else
                        zoomFrameTop = fullZoomHeight - zoomFrameHeight;

                    int zoomFrameLeftTileIndex = (int)Math.Floor(zoomFrameLeft / tileSize) - 1;
                    int zoomFrameTopTileIndex = (int)Math.Floor(zoomFrameTop / tileSize) - 1;

                    int nbrOfHorizontalTiles = (int)Math.Ceiling(zoomFrameWidth / tileSize) + 3;
                    int nbrOfVerticalTiles = (int)Math.Ceiling(zoomFrameHeight / tileSize) + 3;

                    int displayedZoomFrameLeft = zoomFrameLeftTileIndex * lastVersion.TileSize;
                    int displayedZoomFrameTop = zoomFrameTopTileIndex * lastVersion.TileSize;
                    offsetX = zoomFrameLeft - displayedZoomFrameLeft;
                    offsetY = zoomFrameTop - displayedZoomFrameTop;

                    for (int x = 0; x < nbrOfHorizontalTiles; x++)
                    {
                        for (int y = 0; y < nbrOfVerticalTiles; y++)
                        {
                            string fullTilePath = Global.Instance.RootUrl + lastVersion.TilesPath + "/" + RenderLevel + "-" + (zoomFrameLeftTileIndex + x).ToString() + "-" + (zoomFrameTopTileIndex + y).ToString() + ".jpg";
                            Image image = new Image() { };
                            Canvas.SetLeft(image, x * tileSize - offsetX);
                            Canvas.SetTop(image, y * tileSize - offsetY);
                            image.Stretch = Stretch.None;
                            image.Source = new BitmapImage(new Uri(fullTilePath, UriKind.Absolute));

                            _imagesCanvas.Children.Add(image);
                        }
                    }
                    #endregion

                    //double realSizeX = nbrOfHorizontalTiles * tileSize;
                    //double realSizeY = nbrOfVerticalTiles * tileSize;
                    //double scaleX = actualWidth / realSizeX;
                    //double scaleY = actualHeight / realSizeY;
                    //double scale = 0; // Math.Min(scaleX, scaleY);

                    //ScaleTransform scaleTransForm = new ScaleTransform();
                    //scaleTransForm.ScaleX = scale;
                    //scaleTransForm.ScaleY = scale;
                    //_imagesCanvas.RenderTransform = scaleTransForm;

                    #region Locate Pin

                    double commentLocationXZoom = Math.Round(commentLocationX - displayedZoomFrameLeft);
                    double commentLocationYZoom = Math.Round(commentLocationY - displayedZoomFrameTop);
                    PinItem pinItem = CreatePinItem(CanShowLabel ? comment.Code : string.Empty, commentLocationXZoom - offsetX, commentLocationYZoom - offsetY);
                    if (pinItem != null)
                    {
                        //_pinsCanvas.Children.Add(pinItem);
                        _pinsCanvas.Children.Add(pinItem);
                        pinItem.RenderTransform = new RotateTransform() { Angle = -lastVersion.RotateAngle, CenterY = pinItem.Height };
                    }
                    #endregion

                    // apply rotation if available
                    //double xmove = this.Width / 2 - commentLocationXZoom;
                    //double ymove = this.Height / 2 - commentLocationYZoom;
                    //var gp = new TransformGroup();
                    //switch (lastVersion.RotateAngle)
                    //{
                    //    case 90:
                    //    case 270:
                    //        gp.Children.Add(new TranslateTransform() { X = xmove, Y = ymove });
                    //        gp.Children.Add(new RotateTransform() { Angle = lastVersion.RotateAngle, CenterX = this.Width / 2, CenterY = this.Height / 2 });
                    //        break;

                    //    default:
                    //        //(gp as TransformGroup).Children.Add(new TranslateTransform() { X = xmove, Y = ymove });
                    //        break;
                    //}
                    //_imagesCanvas.RenderTransform = gp;
                    //_pinsCanvas.RenderTransform = gp;

                    RotateTransform rt = null;                
                    switch (lastVersion.RotateAngle)
                    {
                        case 90:
                        case 180:
                        case -180:
                        case -90:
                        case 270:
                        case -270:
                            rt = new RotateTransform();
                            rt.CenterX = commentLocationXZoom - offsetX;
                            rt.CenterY = commentLocationYZoom - offsetY;
                            rt.Angle = lastVersion.RotateAngle;
                            break;

                        
                    }
                    _imagesCanvas.RenderTransform = rt;
                    _pinsCanvas.RenderTransform = rt;

                    _layoutRoot.Clip = new RectangleGeometry() { Rect = new Rect(0, 0, actualWidth, actualHeight) };
                }
                else // display all comments without zooming
                {
                    #region display entire image (Big thumb)
                    string fullImagePath = Global.Instance.RootUrl + lastVersion.TilesPath + "/bigthumb.jpg";
                    Image image = new Image() { Stretch = Stretch.Uniform, Source = new BitmapImage(new Uri(fullImagePath, UriKind.Absolute)) };
                    if ((lastVersion.RotateAngle == 90) || (lastVersion.RotateAngle == 270) || (lastVersion.RotateAngle == -90) || (lastVersion.RotateAngle == -270))
                    {
                        image.Width = actualHeight;
                        image.Height = actualWidth;
                    }
                    else
                    {
                        image.Width = actualWidth;
                        image.Height = actualHeight;
                    }
                    _imagesCanvas.Children.Add(image);

                    //Add the pin comments location on the image of the plan
                    //-------------------------------------------------
                    foreach (var comment in Comments)
                    {
                        var commentLocation = comment.Location;
                        double commentLocationXOnImage = 0;
                        double commentLocationYOnImage = 0;
                        if ((lastVersion.RotateAngle == 90) || (lastVersion.RotateAngle == 270) || (lastVersion.RotateAngle == -90) || (lastVersion.RotateAngle == -270))
                        {
                            commentLocationXOnImage = Math.Round((commentLocation.X * actualHeight) / lastVersion.Width);
                            commentLocationYOnImage = Math.Round((commentLocation.Y * actualWidth) / lastVersion.Height);
                        }
                        else
                        {
                            commentLocationXOnImage = Math.Round((commentLocation.X * actualWidth) / lastVersion.Width);
                            commentLocationYOnImage = Math.Round((commentLocation.Y * actualHeight) / lastVersion.Height);
                        }
                        var pinItem = CreatePinItem(CanShowLabel ? comment.Code : string.Empty, commentLocationXOnImage, commentLocationYOnImage);
                        if (pinItem != null)
                        {
                            pinItem.RenderTransform = new RotateTransform() { Angle = -lastVersion.RotateAngle, CenterY = pinItem.Height };
                            _pinsCanvas.Children.Add(pinItem);
                            //_imagesCanvas.Children.Add(pinItem);
                        }
                    }
                    //_imagesCanvas.RenderTransform = CreateBigThumbTransform(lastVersion.RotateAngle, this.Width, this.Height);
                    TransformGroup tg = null;
                    switch (lastVersion.RotateAngle)
                    {
                        case 180:
                        case -180:
                            tg = new TransformGroup();
                            RotateTransform rt1 = new RotateTransform();
                            rt1.CenterX = actualWidth / 2;
                            rt1.CenterY = actualHeight / 2;
                            rt1.Angle = lastVersion.RotateAngle;
                            tg.Children.Add(rt1);
                            break;

                        case -90:
                        case 270:
                            tg = new TransformGroup();
                            RotateTransform rt2 = new RotateTransform();
                            rt2.CenterX = 0;
                            rt2.CenterY = 0;
                            rt2.Angle = lastVersion.RotateAngle;
                            tg.Children.Add(rt2);
                            TranslateTransform tt1 = new TranslateTransform();
                            tt1.Y = actualHeight;
                            tg.Children.Add(tt1);
                            break;

                        case 90:
                        case -270:
                            tg = new TransformGroup();
                            RotateTransform rt3 = new RotateTransform();
                            rt3.CenterX = 0;
                            rt3.CenterY = 0;
                            rt3.Angle = lastVersion.RotateAngle;
                            tg.Children.Add(rt3);
                            TranslateTransform tt2 = new TranslateTransform();
                            tt2.X = actualWidth;
                            tg.Children.Add(tt2);
                            break;


                    }
                    _imagesCanvas.RenderTransform = tg;
                    _pinsCanvas.RenderTransform = tg;

                    #endregion
                }

                
                // clip tiles base on real dimension
                //_clipRect.Rect = new Rect(0, 0, this.Width - 2, this.Height - 2);
            }
        }

        private void DisplayBigThumb()
        { 

        }

        private void DisplayByZoomLevel()
        { 

        }

        private Transform CreateBigThumbTransform(int angle, double w, double h)
        {
            var result = new TransformGroup();
            switch (angle)
            { 
                case 90:
                    result.Children.Add(new RotateTransform() { Angle = angle });
                    result.Children.Add(new TranslateTransform() { X = (w + h) / 2 });
                    break;

                case 180:
                    result.Children.Add(new RotateTransform() { Angle = angle, CenterX = w / 2, CenterY = h / 2 });
                    break;

                case 270:
                    result.Children.Add(new RotateTransform() { Angle = angle });
                    result.Children.Add(new TranslateTransform() { Y = (w + h) / 2 });
                    break;
            }

            return result;
        }

        private PinItem CreatePinItem(string label, double x, double y)
        {
            if (x <= 0 && y <= 0) return null;

            var pin = new PinItem() { Label = label };
            Canvas.SetLeft(pin, x);
            Canvas.SetTop(pin, y - pin.Height);

            return pin;
        }
    }

    public class PinItem : Control
    {
        private TextBlock _pinLabel;

        public string Label { get; set; }

        public PinItem()
        {
            DefaultStyleKey = typeof(PinItem);
            Height = Width = 32;
        }

        public override void OnApplyTemplate()
        {
            // label
            _pinLabel = GetTemplateChild("PinLabel") as TextBlock;
            _pinLabel.Text = Label;

            // text dimension 
            var tsz = GetTextWidth(_pinLabel.Text, _pinLabel.FontSize);
            var left = 21 - tsz.Width / 2;
            var top = 9 - tsz.Height / 2;

            // set position for text on the pin
            Canvas.SetTop(_pinLabel, top);
            Canvas.SetLeft(_pinLabel, left);

            base.OnApplyTemplate();
        }

        private Size GetTextWidth(string text, double fontSize)
        {
            TextBlock txtMeasure = new TextBlock() { FontSize = fontSize, Text = text };
            return new Size(txtMeasure.ActualWidth, txtMeasure.ActualHeight);
        }
    }
}
