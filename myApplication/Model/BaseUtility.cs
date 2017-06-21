using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApplication
{

    public class BaseUtility
    {
        public enum BaseEnum
        {
            None,
            Beer,
            Vokca,
        };

        public string Name { get; set; }
        public string Kind { get; set; }
        public BaseEnum RecipeBase { get; set; }

        public BaseUtility()
        {
            Name = "";
            Kind = "";
            RecipeBase = BaseEnum.None;
        }
    }
}
