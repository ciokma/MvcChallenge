//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hey_url_challenge_code.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Language {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Language() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("hey_url_challenge_code.Resources.Language", typeof(Language).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Browser.
        /// </summary>
        public static string Browser {
            get {
                return ResourceManager.GetString("Browser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PlatForm.
        /// </summary>
        public static string PlatForm {
            get {
                return ResourceManager.GetString("PlatForm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Short Url.
        /// </summary>
        public static string ShortUrl {
            get {
                return ResourceManager.GetString("ShortUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Url you are trying to create already exist..
        /// </summary>
        public static string UrlExist {
            get {
                return ResourceManager.GetString("UrlExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The url you give us is not valid, please try again..
        /// </summary>
        public static string UrlNotValid {
            get {
                return ResourceManager.GetString("UrlNotValid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wait you will be redirected or click on Open button.
        /// </summary>
        public static string WaitOrClickOpen {
            get {
                return ResourceManager.GetString("WaitOrClickOpen", resourceCulture);
            }
        }
    }
}
