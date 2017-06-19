using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Xml;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace myApplication
{
    public class RecipeModel
    {
        public static RecipeModel RecipeModelInstance = null;

        public ObservableCollection<BaseUtility> BaseUtilityList = new ObservableCollection<BaseUtility>();
        public RecipeDataObserver Observer;

        private RecipeModel()
        {
        }

        public static RecipeModel GetInstance()
        {
            if (RecipeModelInstance == null)
            {
                RecipeModelInstance = new RecipeModel();
            }


            return RecipeModelInstance;
        }

        public void SetObserver(RecipeDataObserver observer)
        {
            Observer = observer;
        }

        public void AddItem()
        {
            BaseUtilityList.Add(new BaseUtility());
            Observer.UpdateData();
        }

        public void EditName(int index, string name)
        {
            BaseUtilityList[index].Name = name;
        }

        public void EditKind(int index, string kind)
        {
            BaseUtilityList[index].Kind = kind;
        }

        public void EditBase(int index, BaseUtility.BaseEnum recipeBase)
        {
            BaseUtilityList[index].RecipeBase = recipeBase;
        }

        public async Task<bool> SerializeJsonAsync()
        {
            bool isSucceed = true;

            try
            {
                using (var fs = new FileStream("output.json", FileMode.Create))
                {
                    var serializer = new DataContractJsonSerializer(typeof(ObservableCollection<BaseUtility>));
                    await Task.Run(() =>
                    {
                        serializer.WriteObject(fs, BaseUtilityList);
                    });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                isSucceed = false;
            }

            return isSucceed;
        }

        public async Task<bool> DeserializeJsonAsync()
        {
            bool isSucceed = true;

            try
            {
                using (var fs = new FileStream("output.json", FileMode.Open))
                {
                    var serializer = new DataContractJsonSerializer(typeof(ObservableCollection<BaseUtility>));
                    await Task.Run(() =>
                   {
                       BaseUtilityList = (ObservableCollection<BaseUtility>)serializer.ReadObject(fs);
                       Thread.Sleep(5000);
                   });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                isSucceed = false;
            }

            Observer.UpdateData();
            return isSucceed;
        }
    }
}
