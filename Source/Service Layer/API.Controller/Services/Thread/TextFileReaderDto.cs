using System;

namespace API.Controller.Services.Thread
{
    public class Section
    {
        private string m_Name;
        private TextFieldCollection m_TextFields = null;
        private SectionFormat m_Format = SectionFormat.FixedWidth;

        public Section(string Name, SectionFormat Format)
        {
            m_TextFields = new TextFieldCollection();
            m_Name = Name;
            m_Format = Format;
        }

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value.Length < 1 || value == null || value == String.Empty)
                    throw new Exception("You can not set the Name property to a blank, empty or null value.");
                m_Name = value;
            }
        }


        public TextFieldCollection TextFields
        {
            get { return m_TextFields; }
            set { m_TextFields = value; }
        }

        public SectionFormat SectionFormat
        {
            get { return m_Format; }
            set { m_Format = value; }
        }
    }

    public enum SectionFormat
    {
        FixedWidth,
        Delimited,
        Header,
        Footer
    }

    public class SectionCollection : System.Collections.CollectionBase
    {
        public SectionCollection()
            : base()
        {
        }

        public SectionCollection(SectionCollection texValue)
            : base()
        {
            AddRange(texValue);
        }

        public SectionCollection(Section[] texValue)
            : base()
        {
            AddRange(texValue);
        }

        public Section this[int index]
        {
            get { return (Section)List[index]; }
            set { List[index] = value; }
        }

        public int Add(Section texValue)
        {
            return List.Add(texValue);
        }

        public void AddRange(Section[] texValue)
        {
            int intCounter = 0;
            while (intCounter < texValue.Length)
            {
                Add(texValue[intCounter]);
                intCounter++;
            }
        }

        public void AddRange(SectionCollection texValue)
        {
            int intCounter = 0;
            while (intCounter < texValue.Count)
            {
                Add(texValue[intCounter]);
                intCounter++;
            }
        }

        public bool Contains(Section texValue)
        {
            return List.Contains(texValue);
        }

        public void CopyTo(Section[] texArray, int intIndex)
        {
            List.CopyTo(texArray, intIndex);
        }

        public int IndexOf(Section texValue)
        {
            return List.IndexOf(texValue);
        }

        public void Insert(int intIndex, Section texValue)
        {
            List.Insert(intIndex, texValue);
        }

        public new SectionEnumerator GetEnumerator()
        {
            return new SectionEnumerator(this);
        }

        public void Remove(Section texValue)
        {
            List.Remove(texValue);
        }

        public class SectionEnumerator : System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator iEnBase;
            private System.Collections.IEnumerable iEnLocal;

            public SectionEnumerator(SectionCollection texMappings)
                : base()
            {
                iEnLocal = (System.Collections.IEnumerable)texMappings;
                iEnBase = iEnLocal.GetEnumerator();
            }

            public Section Current
            {
                get
                {
                    return (Section)iEnBase.Current;
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get { return iEnBase.Current; }
            }

            public bool MoveNext()
            {
                return iEnBase.MoveNext();
            }

            bool System.Collections.IEnumerator.MoveNext()
            {
                return iEnBase.MoveNext();
            }

            public void Reset()
            {
                iEnBase.Reset();
            }

            void System.Collections.IEnumerator.Reset()
            {
                iEnBase.Reset();
            }
        }
    }

    public class TextField
    {
        private string m_Name;
        private TypeCode m_DataType = TypeCode.String;
        private int m_Length;
        private int m_StartIndex;
        private object m_Value = null;
        private string m_Format;

        public TextField(string Name, TypeCode DataType, int Length, int StartIndex, string Format)
        {
            m_Name = Name;
            m_DataType = DataType;
            m_Length = Length;
            m_StartIndex = StartIndex;
            m_Value = null;
            m_Format = Format;
        }


        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value.Length < 1 || value == null || value == String.Empty)
                    throw new Exception("You can not set the Name property to a blank, empty or null value.");

                m_Name = value;
            }
        }

        public TypeCode DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }

        public int Length
        {
            get { return m_Length; }
            set
            {
                if (value < 1)
                    throw new Exception("You can not set the Length property to a zero or negative value.");
                m_Length = value;
            }
        }


        public int StartIndex
        {
            get { return m_StartIndex; }
            set
            {
                if (value < 1)
                    throw new Exception("You can not set the StartIndex property to a zero or negative value.");
                m_StartIndex = value;
            }
        }


        public object Value
        {
            get { return m_Value; }
            set
            {
                try
                {
                    if (value.ToString().Trim().Length == 0) //Allow for null values.
                    {
                        m_Value = Convert.DBNull;
                    }
                    else if (m_DataType == TypeCode.Boolean) //special form boolean
                    {
                        if (value.ToString().Trim() == "1")
                            m_Value = true;
                        else if (value.ToString().Trim() == "0")
                            m_Value = false;
                        else
                            m_Value = Convert.ChangeType(value, m_DataType);
                    }
                    else if ((m_DataType == TypeCode.Int32) ||
                        (m_DataType == TypeCode.Int16) || (m_DataType == TypeCode.Int64)) // Remove Comma's from numbers.
                    {
                        string s = value.ToString().Replace(",", "");
                        m_Value = Convert.ChangeType(s, m_DataType);
                    }
                    else
                    {
                        m_Value = Convert.ChangeType(value, m_DataType);
                    }
                }
                catch
                {
                    throw new ArgumentException(String.Format("There was an error converting the value \"{0}\" to a {1} for the field \"{2}\".", value, m_DataType.ToString(), m_Name));
                }
            }
        }

        public string Format
        {
            get { return m_Format; }
            set
            {
                m_Format = value;
            }
        }
    }

    public class TextFieldCollection : System.Collections.CollectionBase
    {
        public TextFieldCollection()
            : base()
        {
        }

        public TextFieldCollection(TextFieldCollection texValue)
            : base()
        {
            AddRange(texValue);
        }


        public TextFieldCollection(TextField[] texValue)
            : base()
        {
            AddRange(texValue);
        }

        public TextField this[int index]
        {
            get { return (TextField)List[index]; }
            set { List[index] = value; }
        }

        public int Add(TextField texValue)
        {
            return List.Add(texValue);
        }

        public void AddRange(TextField[] texValue)
        {
            int intCounter = 0;
            while (intCounter < texValue.Length)
            {
                Add(texValue[intCounter]);
                intCounter++;
            }
        }

        public void AddRange(TextFieldCollection texValue)
        {
            int intCounter = 0;
            while (intCounter < texValue.Count)
            {
                Add(texValue[intCounter]);
                intCounter++;
            }
        }

        public bool Contains(TextField texValue)
        {
            return List.Contains(texValue);
        }

        public void CopyTo(TextField[] texArray, int intIndex)
        {
            List.CopyTo(texArray, intIndex);
        }

        public int IndexOf(TextField texValue)
        {
            return List.IndexOf(texValue);
        }

        public void Insert(int intIndex, TextField texValue)
        {
            List.Insert(intIndex, texValue);
        }

        public new TextFieldEnumerator GetEnumerator()
        {
            return new TextFieldEnumerator(this);
        }

        public void Remove(TextField texValue)
        {
            List.Remove(texValue);
        }

        public class TextFieldEnumerator : System.Collections.IEnumerator
        {
            private System.Collections.IEnumerator iEnBase;
            private System.Collections.IEnumerable iEnLocal;

            public TextFieldEnumerator(TextFieldCollection texMappings)
                : base()
            {
                iEnLocal = (System.Collections.IEnumerable)texMappings;
                iEnBase = iEnLocal.GetEnumerator();
            }

            public TextField Current
            {
                get
                {
                    return (TextField)iEnBase.Current;
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get { return iEnBase.Current; }
            }

            public bool MoveNext()
            {
                return iEnBase.MoveNext();
            }

            bool System.Collections.IEnumerator.MoveNext()
            {
                return iEnBase.MoveNext();
            }

            public void Reset()
            {
                iEnBase.Reset();
            }

            void System.Collections.IEnumerator.Reset()
            {
                iEnBase.Reset();
            }
        }
    }
}
