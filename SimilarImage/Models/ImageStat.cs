using System;
using System.ComponentModel;

namespace SimilarImage.Models
{
    public class ImageStat
    {
        public ImageStat()
        {
            IsDuplicate = 0;
        }

        public int Id { get; set; }
        public int Rectangles { get; set; }
        public int Pixels { get; set; }

        [Browsable(false)]
        public decimal Luminance { get; set; }

        [Browsable(false)]
        public int Blue { get; set; }

        [Browsable(false)]
        public int Red { get; set; }

        [Browsable(false)]
        public int Green { get; set; }

        [Browsable(false)]
        public float Cb { get; set; }

        [Browsable(false)]
        public float Cr { get; set; }

        [Browsable(false)]
        public float Y { get; set; }

        public string ImagePath { get; set; }
        public string SetName { get; set; }
        public bool IsExcludedFromSearchSet { get; set; }
        public int IsDuplicate { get; set; }
        public int IsMoved { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [Browsable(false)]
        public int BlueMax { get; set; }

        [Browsable(false)]
        public int BlueMin { get; set; }

        [Browsable(false)]
        public int RedMax { get; set; }

        [Browsable(false)]
        public int RedMin { get; set; }

        [Browsable(false)]
        public int GreenMax { get; set; }

        [Browsable(false)]
        public int GreenMin { get; set; }

        public string MD5 { get; set; }
        public long Length { get; set; }
        public double Similarity { get; set; }
    }
}

//CREATE TABLE 'ImageStat' (
//	'id'	INTEGER PRIMARY KEY AUTOINCREMENT,
//	'rectangles'	INTEGER,
//	'luminance'	NUMERIC,
//	'blue'	INTEGER,
//	'red'	INTEGER,
//	'green'	INTEGER,
//	'cb'	INTEGER,
//	'cr'	INTEGER,
//	'y'	INTEGER,
//	'pixels'	INTEGER,
//	'imagePath'	TEXT,
//	'IsDuplicate'	INTEGER DEFAULT 0,
//	'IsMoved'	INTEGER DEFAULT 0,
//	'BlueMax'	INTEGER,
//	'BlueMin'	INTEGER,
//	'RedMax'	INTEGER,
//	'RedMin'	INTEGER,
//	'GreenMax'	INTEGER,
//	'GreenMin'	INTEGER,
//	'length'	INTEGER,
//);