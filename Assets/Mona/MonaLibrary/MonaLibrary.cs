#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mona
{
    [System.Serializable]
    public class MonaLibrary
    {
        public Dictionary<string, Texture2D> icons;
        public Dictionary<int, Tools> tools;
        public Dictionary<int, Assets> assets;
        [System.Serializable]
        public class Sections
        {
            [System.Serializable]
            public class Fields 
            {
                public string name;
                public string table;
                public string type;
            }
            [System.Serializable]
            public class Item
            {
                public Fields fields;
            }
            public List<Item> records;
        }
        public Sections sections;
        [System.Serializable]
        public class Tools
        {
            [System.Serializable]
            public class Fields 
            {
                public Fields()
                {
                    file = new List<File>();
                    icon = new List<Thumbnail>();
                }
                public string id;
                public string name;
                public List<File> file;
                public List<Thumbnail> icon;
                public string description;
                public string docslink;
            }
            [System.Serializable]
            public class Item{

                public Fields fields;
            }
            [System.Serializable]
            public class File
            {
                public string id;
                public string url;
                public string filename;
                public int size;
                public string type;

            }
            [System.Serializable]
            public class Thumbnail
            {
                public string id;
                public int width;
                public int height;
                public string url;
                public string filename;
                public int size;
                public string type;
            }
            public List<Item> records;
        }
        public Tools tempTool;
        [System.Serializable]
        public class Assets
        {
            [System.Serializable]
            public class Fields 
            {
                public Fields()
                {
                    file = new List<File>();
                    icon = new List<Thumbnail>();
                }
                public string id;
                public string name;
                public string category;
                public List<File> file;
                public int polycount;
                public string artist;
                public List<Thumbnail> icon;
                public string alias;
                public bool invalid;
            }
            [System.Serializable]
            public class Item{

                public Fields fields;
            }
            [System.Serializable]
            public class File
            {
                public string id;
                public string url;
                public string filename;
                public int size;
                public string type;

            }
            [System.Serializable]
            public class Thumbnail
            {
                public string id;
                public int width;
                public int height;
                public string url;
                public string filename;
                public int size;
                public string type;
            }
            public List<Item> records;
        }
        public Assets tempAsset;
    }
}
#endif