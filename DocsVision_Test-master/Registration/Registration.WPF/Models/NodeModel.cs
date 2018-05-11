﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration.WPF.Models
{
    public class Node : ViewModels.Notifier
    {
        public string Name { get; set; }
        public Folder Folder { get; set; }

        private bool _isSelected { get; set; }

        public Boolean IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

       
    }

    public class DirectoryNode : Node
    {
        public DirectoryNode()
        {
            Items = new List<DirectoryNode>();
        }

        public List<DirectoryNode> Items { get; set; }

        public void AddDirNode(DirectoryNode directoryNode)
        {
            Items.Add(directoryNode);
        }

        public List<Node> Traverse(DirectoryNode it)
        {
            var nodes = new List<Node>();

            foreach (var itm in it.Items)
            {
                Traverse(itm);
                nodes.Add(itm);
            }

            return nodes;
        }


    }
}
