﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using TermLib;

namespace SerializerLib
{
    public class Serializer 
    {
        public List<Term> TermList=new List<Term>();
        public static int Class;
        public static string Subject;

        public string DefaultPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Teacherry\\";

        public string Path;
        public string FileName;
        BinaryFormatter formatter = new BinaryFormatter();

        public Serializer(int cl,string subj)
        {
            if(cl<=11&&cl>=1)
                Class = cl;
            Subject = subj;
            FileName = Class + ".dat";
            Path = DefaultPath + Subject + "\\";
        }
        public void Serialize()
        {
            CheckForPathExist();
            using (FileStream fs = new FileStream(Path+FileName, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, TermList);
                Console.WriteLine("Сериализован");
            }
        }
        public void Deserialize()
        {
            CheckForPathExist();
            using (FileStream fs = new FileStream(Path+FileName, FileMode.OpenOrCreate))
            {
                TermList = (List<Term>)formatter.Deserialize(fs);
                Console.WriteLine("Десериализован");
            }
        }
        private void CheckForPathExist()
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }
    }
}