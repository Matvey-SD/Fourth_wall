﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fourth_wall.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Fourth_wall.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap Background {
            get {
                object obj = ResourceManager.GetObject("Background", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to В сундуке лежат две таблетки, синяя и красная, а также записка: &quot;Красная +2 дамага, синяя - хилл&quot;. Мда, с фантазией всё плохо. Вы хотите съесть красную? .
        /// </summary>
        internal static string ChestOpen_Message {
            get {
                return ResourceManager.GetString("ChestOpen_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Со смертью этого персонажа нить вашей судьбы обрывается. Загрузите сохранённую игру дабы восстановить течение судьбы, или живите дальше в проклятом мире, который сами и создали.
        /// </summary>
        internal static string DeathMessage {
            get {
                return ResourceManager.GetString("DeathMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выход.
        /// </summary>
        internal static string Exit {
            get {
                return ResourceManager.GetString("Exit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Конец игры!.
        /// </summary>
        internal static string GameEnd {
            get {
                return ResourceManager.GetString("GameEnd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поздравляю, вы прошли игру. Вау, да вы круты..
        /// </summary>
        internal static string GameEndsText {
            get {
                return ResourceManager.GetString("GameEndsText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 4-th Wall.
        /// </summary>
        internal static string GameName {
            get {
                return ResourceManager.GetString("GameName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ваше здоровье: .
        /// </summary>
        internal static string HPInfo {
            get {
                return ResourceManager.GetString("HPInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Управление: WASD - движение, SPACE - удар. Всё просто!.
        /// </summary>
        internal static string Info {
            get {
                return ResourceManager.GetString("Info", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выход.
        /// </summary>
        internal static string MainMenu_Exit {
            get {
                return ResourceManager.GetString("MainMenu_Exit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Сыграем, .
        /// </summary>
        internal static string MainMenu_Start {
            get {
                return ResourceManager.GetString("MainMenu_Start", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вы точно уверены, что хотите выйти?.
        /// </summary>
        internal static string On_Exit {
            get {
                return ResourceManager.GetString("On_Exit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Вы открываете сундук.
        /// </summary>
        internal static string OpenChest {
            get {
                return ResourceManager.GetString("OpenChest", resourceCulture);
            }
        }
    }
}
