
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ControlLibrary
{
    /// <summary>
    /// Represents a control that contains multiple pages that share the same space on screen.
    /// </summary>
    public class ModernTab
        : ContentControl
    {
        /// <summary>
        /// Identifies the ContentLoader dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentLoaderProperty = DependencyProperty.Register("ContentLoader", typeof(IContentLoader), typeof(ModernTab), new PropertyMetadata(new DefaultContentLoader()));
        /// <summary>
        /// Identifies the Layout dependency property.
        /// </summary>
        public static readonly DependencyProperty LayoutProperty = DependencyProperty.Register("Layout", typeof(TabLayout), typeof(ModernTab), new PropertyMetadata(TabLayout.Tab));
        /// <summary>
        /// Identifies the Links dependency property.
        /// </summary>
        public static readonly DependencyProperty LinksProperty = DependencyProperty.Register("Links", typeof(LinkCollection), typeof(ModernTab), new PropertyMetadata(OnLinksChanged));
        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedSourceProperty = DependencyProperty.Register("SelectedSource", typeof(Uri), typeof(ModernTab), new PropertyMetadata(OnSelectedSourceChanged));
        /// <summary>
        /// Identifies the IsLoadingContent dependency property.
        /// </summary>
        public static readonly DependencyProperty IsLoadingContentProperty = DependencyProperty.Register("IsLoadingContent", typeof(bool), typeof(ModernTab), new PropertyMetadata(false));
        /// <summary>
        /// Identifies the SelectedLink dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedLinkProperty = DependencyProperty.Register("SelectedLink", typeof(Link), typeof(ModernTab), new PropertyMetadata(OnSelectedLinkChanged));

        private ListBox linkList;


        /// <summary>
        /// Initializes a new instance of the <see cref="ModernTab"/> control.
        /// </summary>
        public ModernTab()
        {
            this.DefaultStyleKey = typeof(ModernTab);
            // create a default links collection
            SetCurrentValue(LinksProperty, new LinkCollection());
            this.Links.CollectionChanged += Links_CollectionChanged;
        }

        void Links_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                SelectedLink.ContentPresenter = null;
                if (this.Links.Count == 0)
                {
                    SelectedLink = null;
                    this.Links.CollectionChanged -= Links_CollectionChanged;
                    return;
                }
                if (SelectedLink == e.OldItems[0])
                    SelectedLink = this.Links.LastOrDefault();

            }
        }

     

        private static void OnLinksChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ModernTab)o).UpdateSelection();
        }

        private static void OnSelectedSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ModernTab)o).UpdateSelection();
        }

        private static void OnSelectedLinkChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((ModernTab)o).UpdateSelection();
        }

        private void UpdateSelection()
        {
            if (this.linkList == null || this.Links == null)
            {
                return;
            }

            // sync list selection with current source
            this.linkList.SelectedItem = this.Links.FirstOrDefault(l => l == this.SelectedLink);
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.linkList != null)
            {
                this.linkList.SelectionChanged -= OnLinkListSelectionChanged;
            }

            this.linkList = GetTemplateChild("LinkList") as ListBox;
            if (this.linkList != null)
            {
                this.linkList.SelectionChanged += OnLinkListSelectionChanged;

            }
            UpdateSelection();
        }


        private void OnLinkListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var link = this.linkList.SelectedItem as Link;
            if (link != null && link != this.SelectedLink)
            {
                SetCurrentValue(SelectedLinkProperty, link);
            }
        }

        /// <summary>
        /// Gets or sets the content loader.
        /// </summary>
        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating how the tab should be rendered.
        /// </summary>
        public TabLayout Layout
        {
            get { return (TabLayout)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of links that define the available content in this tab.
        /// </summary>
        public LinkCollection Links
        {
            get { return (LinkCollection)GetValue(LinksProperty); }
            set { SetValue(LinksProperty, value); }
        }

        /// <summary>
        /// Gets or sets the source URI of the selected link.
        /// </summary>
        /// <value>The source URI of the selected link.</value>
        public Uri SelectedSource
        {
            get { return (Uri)GetValue(SelectedSourceProperty); }
            set { SetValue(SelectedSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the source URI of the selected link.
        /// </summary>
        /// <value>The source URI of the selected link.</value>
        public Link SelectedLink
        {
            get { return (Link)GetValue(SelectedLinkProperty); }
            set { SetValue(SelectedLinkProperty, value); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is currently loading content.
        /// </summary>
        public bool IsLoadingContent
        {
            get { return (bool)GetValue(IsLoadingContentProperty); }
            set { SetValue(IsLoadingContentProperty, value); }
        }

        /// <summary>
        /// Add new item to Tab control
        /// </summary>
        /// <param name="item">FrameworkElement item to add</param>
        public void AddTabItem(FrameworkElement item, string Xname,object displayName = null)
        {
            if (item == null)
                return;
            //item.Uid
            
            Link linkItem = new Link();
            linkItem.DisplayName = displayName == null ? Xname : displayName;
            linkItem.Source = new Uri("/CMS_URI" + Guid.NewGuid().ToString("B"), UriKind.RelativeOrAbsolute);
            linkItem.ContentPresenter = item;
            if (this.Links == null)
                this.Links = new LinkCollection();
            var selectedItem = this.Links.FirstOrDefault(f => f.XName == Xname);
            if (selectedItem == null)
            {
                item.Name = Xname;
                this.Links.Add(linkItem);
                selectedItem = linkItem;
            }

            SelectedLink = selectedItem;
        }

        /// <summary>
        /// Select tab item by name of tab
        /// </summary>
        /// <param name="nameItem">string name</param>
        public void SelectTabByName(string nameItem)
        {
            var selectedItem = this.Links.FirstOrDefault(f => f.XName == nameItem);
            if (selectedItem == null)
                throw new Exception("the name " + nameItem + " not exist in tabcontrol");
            SelectedLink = selectedItem;
        }

        public bool CheckTabByName(string nameItem)
        {
            var selectedItem = this.Links.FirstOrDefault(f => f.XName == nameItem);
            return selectedItem !=null;
        }
    }
}
