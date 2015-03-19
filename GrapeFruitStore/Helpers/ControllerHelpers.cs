using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrapeFruitStore.Models;

namespace GrapeFruitStore.Helpers
{
    /*
     * We can make this code a little cleaner by adding a “ControllerHelpers” class to the project, 
     * and implement an “AddRuleViolations” extension method within it that adds a helper method to the 
     * ASP.NET MVC ModelStateDictionary class.   This extension method can encapsulate the logic necessary 
     * to populate the ModelStateDictionary with a list of RuleViolation errors.
     * 
     */

    public static class Constants
    {
        public const string AdministratorsRoleName = "root";
        public const string ImagesPath = "/Content/Images/";
    }

    public static class ModelStateHelpers
    {

        public static void AddModelErrors(this ModelStateDictionary modelState, IEnumerable<RuleViolation> errors)
        {
            /*
             * Controller classes have a “ModelState” property collection which provides a way to
             * indicate that errors exist with a model object being passed to a View.
             * Error entries within the ModelState collection identify the name of the model
             * property with the issue and allow a human-friendly error message to be specified. 
             * 
             */

            foreach (RuleViolation issue in errors)
            {
                modelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
            }
        }
    }
}
