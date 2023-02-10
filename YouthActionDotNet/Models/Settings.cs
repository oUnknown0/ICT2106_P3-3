using System.Collections.Generic;

namespace YouthActionDotNet.Models
{
    public class Settings
    {
        public Dictionary<string, ColumnHeader> ColumnSettings;

        public Dictionary<string, InputType> FieldSettings;
    }

    public class UserSettings: Settings{

        public UserSettings(){
            ColumnSettings = new Dictionary<string, ColumnHeader>();
            FieldSettings = new Dictionary<string, InputType>();
            FieldSettings.Add("UserId", new InputType { type = "number", displayLabel = "User Id", editable = false, primaryKey = true });
            FieldSettings.Add("username", new InputType { type = "text", displayLabel = "Username", editable = true, primaryKey = false });
            FieldSettings.Add("Email", new InputType { type = "text", displayLabel = "Email", editable = true, primaryKey = false });
            FieldSettings.Add("Password", new InputType { type = "text", displayLabel = "Password", editable = true, primaryKey = false });
            FieldSettings.Add("Role", new DropdownInputType { type = "dropdown", displayLabel = "Role", editable = true, primaryKey = false, options = new List<DropdownOption> {
                new DropdownOption { value = "Admin", label = "Admin" },
                new DropdownOption { value = "Employee", label = "Employee" },
                new DropdownOption { value = "Volunteer", label = "Volunteer" },
                new DropdownOption { value = "Donor", label = "Donor" },
            } });
        }

        
    }


    public class InputType{
        public string type { get; set; }
        public string displayLabel { get; set; }
        public bool editable { get; set; }   
        public bool primaryKey { get; set; }
        public string toolTip { get; set; }
        
    }

    public class DropdownInputType<T> : InputType where T : class{
        public List<DropdownOption<T>> options { get; set; }
    }
    
    public class DropdownInputType : InputType{
        public List<DropdownOption> options { get; set; }
        public bool allowEmpty { get; set; }
    }

    public class DropdownOption<T> where T : class{
        public T value { get; set; }
        public string label { get; set; }
    }

    public class DropdownOption{
        public string value { get; set; }
        public string label { get; set; }
    }

    public class ColumnHeader{
        public string displayHeader { get; set; }
    }


}