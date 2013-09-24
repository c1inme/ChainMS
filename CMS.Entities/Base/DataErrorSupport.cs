using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System;

namespace CMS.Entities
{
    /// <summary>
    /// This class provides an implementation for the <see cref="IDataErrorInfo"/> interface which uses the
    /// validation classes found in the <see cref="System.ComponentModel.DataAnnotations"/> namespace.
    /// </summary>
    public sealed class DataErrorSupport : IDataErrorInfo
    {
        private readonly object instance;


        /// <summary>
        /// Initializes a new instance of the <see cref="DataErrorSupport"/> class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <exception cref="ArgumentNullException">instance must not be <c>null</c>.</exception>
        public DataErrorSupport(object instance)
        {
            if (instance == null) { throw new ArgumentNullException("instance"); }
            this.instance = instance;
        }


        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error { get { return this[""]; } }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="memberName">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        public string this[string memberName]
        {
            get
            {
                List<ValidationResult> validationResults = new List<ValidationResult>();

                if (string.IsNullOrEmpty(memberName))
                {
                    Validator.TryValidateObject(instance, new ValidationContext(instance, null, null), validationResults, true);
                }
                else
                {
                    PropertyDescriptor property = TypeDescriptor.GetProperties(instance)[memberName];
                    if (property == null)
                    {
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                            "The specified member {0} was not found on the instance {1}", memberName, instance.GetType()));
                    }
                    Validator.TryValidateProperty(property.GetValue(instance),
                        new ValidationContext(instance, null, null) { MemberName = memberName }, validationResults);
                }

                return string.Join(Environment.NewLine, validationResults.Select(x =>
                    ParseError(x.ErrorMessage)
                    ));
            }
        }

        private string ParseError(string errror)
        {
            if (string.IsNullOrEmpty(errror))
                return null;

            string[] tokent = errror.Split('$');
            if (tokent.Count() <= 1)
                return Global.Instance.GetLangByKey(errror);


            string messeage = Global.Instance.GetLangByKey(tokent[0]);
            List<string> strs = new List<string>();
            for (int i = 1; i < tokent.Count(); i++)
                strs.Add(tokent[i]);
            return String.Format(messeage, strs.ToArray());
        }

    }
}
