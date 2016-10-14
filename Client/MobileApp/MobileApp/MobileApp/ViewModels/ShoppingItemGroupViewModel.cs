﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MobileApp.ViewModels
{
    public class ShoppingItemGroupViewModel : ObservableCollection<ShoppingItemViewModel>
    {
        public ShoppingItemGroupViewModel(string group)
        {
            Group = group;
            CollectionChanged += ShoppingItemGroupViewModel_CollectionChanged;
        }

        private void ShoppingItemGroupViewModel_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (!_toggling)
                RefreshCache();
        }

        public string Group { get; set; }

        private bool _isExpanded = true;
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsExpanded)));

                    // update the expand icon
                    ExpandIcon = _isExpanded ? "md-expand-more" : "md-expand-less";
                }
            }
        }

        private string _expandIcon = "md-expand-more";
        public string ExpandIcon
        {
            get
            {
                return _expandIcon;
            }
            set
            {
                if (_expandIcon != value)
                {
                    _expandIcon = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ExpandIcon)));
                }
            }
        }

        private List<ShoppingItemViewModel> _itemCache = new List<ShoppingItemViewModel>();

        private void RefreshCache()
        {
            _itemCache.Clear();
            _itemCache.AddRange(this);
        }

        private void LoadCache()
        {
            ClearItems();
            foreach (var item in _itemCache)
            {
                Add(item);
            }
        }

        private bool _toggling = false;

        public void Toggle()
        {
            _toggling = true;

            if (!IsExpanded)
            {
                LoadCache();
            }
            else
            {
                ClearItems();
            }

            IsExpanded = !IsExpanded;

            _toggling = false;
        }
    }
}