using Assets.Project.Scripts.Enums;
using System;
using System.Collections.Generic;

namespace Assets.Project.Scripts.Data
{
    [Serializable]
    public class CategoryDefinition
    {
        public CategoryType CategoryType;
        public List<ItemDefinition> ItemDefinitions;
    }
}
