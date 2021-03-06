﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace myApplication
{
    public class RecipeDataContorller
    {
        private RecipeModel RecipeModelInstance = RecipeModel.GetInstance();

        public RecipeDataContorller()
        {

        }

        public List<BaseUtility.BaseEnum> GetComboBoxSource()
        {
            List<BaseUtility.BaseEnum> baseList = new List<BaseUtility.BaseEnum>();
            foreach (BaseUtility.BaseEnum temp in Enum.GetValues(typeof(BaseUtility.BaseEnum)))
            {
                baseList.Add(temp);
            }

            return baseList;
        }

        public void AddItem()
        {
            RecipeModelInstance.AddItem();
        }

        public void EditItem(int index, string header, FrameworkElement element)
        {
            int listCount = RecipeModelInstance.BaseUtilityList.Count;
            if (listCount > index)
            {
                if (element is TextBox)
                {
                    string value = ((TextBox)element).Text;
                    if (header == "Name")
                    {
                        RecipeModelInstance.EditName(index, value);
                    }
                    else if (header == "Kind")
                    {
                        RecipeModelInstance.EditKind(index, value);
                    }
                }
                else if(header == "Base")
                {
                    BaseUtility.BaseEnum value = (BaseUtility.BaseEnum)Enum.ToObject(typeof(BaseUtility.BaseEnum), ((ComboBox)element).Text);
                    RecipeModelInstance.EditBase(index, value);
                }
            }
        }

        public void SerializeJson()
        {
           RecipeModelInstance.SerializeJsonAsync();
        }

        public void DeserializeJson()
        {
            RecipeModelInstance.DeserializeJsonAsync();
        }
    }
}
