﻿namespace later_list
{
    public class SerieModel
    {
        public string Name { get; private set; }
        public string Genre { get; private set; }

        public SerieModel(string name, string genre)
        {
            Name = name;
            Genre = genre;
        }
    }
}
