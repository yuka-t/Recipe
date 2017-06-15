using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApplication
{

    public class RecipeModel
    {
        public static RecipeModel RecipeModelInstance = new RecipeModel();
        public List<BaseUtility> BaseUtilityList = new List<BaseUtility>();

        private RecipeModel()
        {
            BaseUtilityList.Add(new RedEye());
        }

        public static RecipeModel GetInstance()
        {
            if (RecipeModelInstance == null)
            {
                RecipeModelInstance = new RecipeModel();
            }


            return RecipeModelInstance;
        }
    }
}
