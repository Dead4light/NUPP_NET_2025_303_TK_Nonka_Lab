using System;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics; 

namespace NetworkLab1 
{
    
    
    
    public class Explainer : IDisposable, ICloneable
    {
        
        [Required(ErrorMessage = "Поле Jetener є обов'язковим")]
        public string Jetener { get; set; } = "DefaultValue";

        
        public int Id { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;

       
        
        
        public void GotStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            using (var reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                Debug.WriteLine($"Отримано дані: {content}");
                
            }
        }

        
        
       
        public object Clone()
        {
            return new Explainer()
            {
                Jetener = this.Jetener,
                Id = this.Id
                
            };
        }

        
        
        
        public void Dispose()
        {
            
            Jetener = null;

            
            Debug.WriteLine("Ресурси Explainer звільнено");
        }

        
        public string GetInfo()
        {
            return $"ID: {Id}, Jetener: {Jetener}, Створено: {CreatedAt:yyyy-MM-dd}";
        }
    }
}