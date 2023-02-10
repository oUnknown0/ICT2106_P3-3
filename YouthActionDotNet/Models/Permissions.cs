using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models{

    public class Permissions{
        public Permissions(){
            string json = "";
            this.Id = Guid.NewGuid().ToString();
            using (StreamReader r = new StreamReader("./PermissionTemplate/permission.json")){
                json = r.ReadToEnd();
                r.Close();
            }
            List<Permission> items = JsonConvert.DeserializeObject<List<Permission>>(json);
            this.Permission = JsonConvert.SerializeObject(items);
            
        }

        public string Id { get; set; }

        public string Role { get; set; }

        public string Permission { get; set; }

        public static void UpdateDefaultPermissions(string name){
            string json = "";
            using (StreamReader r = new StreamReader("./PermissionTemplate/permission.json")){
                json = r.ReadToEnd();
                r.Close();
            }


            using (StreamWriter w = new StreamWriter("./PermissionTemplate/permission.json")){
                
                List<Permission> items = JsonConvert.DeserializeObject<List<Permission>>(json);
                for (int i = 0; i < items.Count; i++){
                    if (items[i].Module == name){
                        return;
                    }
                }
                
                items.Add(new Permission(name));
                string output = JsonConvert.SerializeObject(items, Formatting.Indented);
                w.Write(output);
                w.Close();
            }
        }

        public static void RemoveDefaultPermissions(string name){
            string json = "";
            using (StreamReader r = new StreamReader("./PermissionTemplate/permission.json")){
                json = r.ReadToEnd();
                r.Close();
            }

            using (StreamWriter w = new StreamWriter("./PermissionTemplate/permission.json")){
                List<Permission> items = JsonConvert.DeserializeObject<List<Permission>>(json);
                for (int i = 0; i < items.Count; i++){
                    if (items[i].Module == name){
                        items.RemoveAt(i);
                        break;
                    }
                }
                string output = JsonConvert.SerializeObject(items, Formatting.Indented);
                w.Write(output);
                w.Close();
            }
        }

        public static List<string> GetAllModules(){
            string json = "";
            using (StreamReader r = new StreamReader("./PermissionTemplate/permission.json")){
                json = r.ReadToEnd();
                r.Close();
            }
            List<string> modules = new List<string>();
            List<Permission> items = JsonConvert.DeserializeObject<List<Permission>>(json);
            foreach (var item in items){
                modules.Add(item.Module);
            }
            return modules;
        }

    }

    public class Permission{

        public Permission(string module){
            this.Module = module;
            this.Create = true;
            this.Read = true;
            this.Update = true;
            this.Delete = true;
        }
        public string Module { get; set; }
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}