using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using System.Runtime.Serialization;
using System.Reflection;

namespace GrapeFruitStore.Models
{
    //[Bind(Include = "DepartmentID,Title,Description,ImageUrl,UnitsInStock,Price")]
    /*
     * фигня под названием binding rules. это для секурности, чтобы кто попало не мог присваивать другим
     * полям класса всякую другую, особую фигню (manipulated via binding).
     * 
     * кроме того, вся логика тоже сосредоточена в модели, контроллер и вид о ней ничего не знают.
     

    public partial class OrderItem
	{
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Quantity))
                yield return new RuleViolation("Необходим заголовок", "Title");

            if (String.IsNullOrEmpty(Description))
                yield return new RuleViolation("Необходимо описание", "Description");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Не удалось сохранить из-за ошибок.");
        }
    }*/
}