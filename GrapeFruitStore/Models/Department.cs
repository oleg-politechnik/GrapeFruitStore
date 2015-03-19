using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
//using GrapeFruitStore.Helpers;

namespace GrapeFruitStore.Models
{
    [Bind(Include = "Title,Description")]
    /*
     * фигня под названием binding rules. это для секурности, чтобы кто попало не мог присваивать другим
     * полям класса всякую другую, особую фигню (manipulated via binding).
     * 
     * кроме того, вся логика тоже сосредоточена в модели, контроллер и вид о ней ничего не знают.
     */
    public partial class Department
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Title))
                yield return new RuleViolation("Необходим заголовок", "Title");

            if (String.IsNullOrEmpty(Description))
                yield return new RuleViolation("Необходимо описание", "Description");

            //         if (!PhoneValidator.IsValidNumber(ContactPhone, Country))
            //               yield return new RuleViolation("Phone# does not match country", "ContactPhone");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Не удалось сохранить из-за ошибок.");
        }
    }
}