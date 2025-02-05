﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#nullable disable

using System.Globalization;

namespace System.ComponentModel.Design.Serialization
{
    public sealed partial class CodeDomLocalizationProvider
    {
        /// <summary>
        ///  This is a culture info converter that knows how to provide
        ///  a restricted list of cultures based on the SupportedCultures
        ///  property of the extender.  If the extender can't be found
        ///  or the SupportedCultures property returns null, this
        ///  defaults to the stock implementation.
        /// </summary>
        internal sealed class LanguageCultureInfoConverter : CultureInfoConverter
        {
            /// <summary>
            ///  Retrieves the Name for a input CultureInfo.
            /// </summary>
            protected override string GetCultureName(CultureInfo culture)
            {
                return culture.DisplayName;
            }

            /// <summary>
            ///  Gets a collection of standard values collection for a System.Globalization.CultureInfo
            ///  object using the specified context.
            /// </summary>
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                StandardValuesCollection values = null;

                if (context.PropertyDescriptor is not null)
                {
                    ExtenderProvidedPropertyAttribute attr = context.PropertyDescriptor.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;

                    if (attr is not null)
                    {
                        LanguageExtenders provider = attr.Provider as LanguageExtenders;

                        if (provider is not null)
                        {
                            values = provider.SupportedCultures;
                        }
                    }
                }

                values ??= base.GetStandardValues(context);

                return values;
            }
        }
    }
}
