using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ControlLibrary
{
    public class CMSTreeView : TreeView
    {


        public override void OnApplyTemplate()
        {
            this.DefaultStyleKey = typeof(TreeView);
            base.OnApplyTemplate();
        }

        /// <summary>
        /// Identifies the ItemToSelected dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemToSelectedProperty = DependencyProperty.Register("ItemToSelected", typeof(object), typeof(CMSTreeView), new PropertyMetadata(OnItemToSelectedChanged));
        private static void OnItemToSelectedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((CMSTreeView)o).SetItemSelected((CMSTreeView)o, e.NewValue);
        }

        public object ItemToSelected
        {
            get { return this.SelectedItem; }
            set { SetValue(ItemToSelectedProperty, value); }
        }

        private void SetItemSelected(ItemsControl mainItems, object item)
        {
            if (item == null)
                return;
            TreeViewItem currentItem = null;
            TreeViewItem itemsSub = null;

            foreach (var items in mainItems.Items)
            {
                if (items.Equals(item))
                {

                    currentItem = mainItems.ItemContainerGenerator.ContainerFromItem(items) as TreeViewItem;

                    if (currentItem != null && !currentItem.IsSelected)
                    {
                        currentItem.IsSelected = true;
                        return;
                    }
                }

                itemsSub = mainItems.ItemContainerGenerator.ContainerFromItem(items) as TreeViewItem;
                if (itemsSub != null && itemsSub.Items != null && itemsSub.Items.Count > 0)
                {
                    itemsSub.IsExpanded = true;
                    mainItems.UpdateLayout();
                    SetItemSelected(itemsSub, item);
                }
            }
        }

        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            //ItemToSelected = e.NewValue;
            base.OnSelectedItemChanged(e);
        }


        public CMSTreeView()
        {
            this.Loaded += CMSTreeView_Loaded;
        }

        void CMSTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            if (ExpandAll)
                OnExpandAll(this);
        }

        public static readonly DependencyProperty ExpandAllProperty = DependencyProperty.Register("ExpandAll", typeof(bool), typeof(CMSTreeView), new PropertyMetadata(OnExpandAllChanged));
        private static void OnExpandAllChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
                ((CMSTreeView)o).OnExpandAll((CMSTreeView)o);
        }

        public void OnExpandAll(ItemsControl mainItems)
        {

            TreeViewItem itemsSub = null;
            foreach (var items in mainItems.Items)
            {
                itemsSub = mainItems.ItemContainerGenerator.ContainerFromItem(items) as TreeViewItem;
                if (itemsSub != null && itemsSub.Items != null && itemsSub.Items.Count > 0)
                {
                    if (!itemsSub.IsExpanded)
                    {
                        itemsSub.IsExpanded = true;
                        mainItems.UpdateLayout();
                    }
                    OnExpandAll(itemsSub);
                }
            }
        }

        public bool ExpandAll
        {
            get { return (bool)GetValue(ExpandAllProperty); }
            set { SetValue(ExpandAllProperty, value); }
        }
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
        }
    }
}
