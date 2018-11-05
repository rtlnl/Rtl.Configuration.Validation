using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace Rtl.Configuration.Validation
{
    class OptionsValidator<T> : IOptionsValidator where T : class, new()
    {
        private readonly T _options;

        public OptionsValidator(IOptions<T> options)
        {
            _options = options.Value;
        }

        public void Validate()
        {
            Validate(_options);
        }

        private void Validate(object obj)
        {
            Validator.ValidateObject(obj, new ValidationContext(obj), validateAllProperties: true);

            var type = obj.GetType();
            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach(var propertyInfo in propertyInfos)
            {
                if (IsUserDefinedType(propertyInfo.PropertyType))
                {
                    ValidateUserDefinedObject(propertyInfo.GetValue(obj));
                }
                else if (IsCollectionType(propertyInfo.PropertyType))
                {
                    ValidateCollection((IEnumerable)propertyInfo.GetValue(obj));
                }
            }
        }

        private bool IsUserDefinedType(Type type)
        {
            if (type.IsPrimitive || type.IsEnum)
            {
                return false;
            }

            if (type.Namespace != null && type.Namespace.StartsWith("System"))
            {
                return false;
            }

            return true;
        }

        private bool IsCollectionType(Type type)
        {
            return type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
        }

        private void ValidateUserDefinedObject(object obj)
        {
            if (obj != null)
            {
                Validate(obj);
            }
        }

        private void ValidateCollection(IEnumerable collection)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    Validate(item);
                }
            }
        }
    }
}
