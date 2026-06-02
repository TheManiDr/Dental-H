namespace Dental_H.Model
{
    public class ComboItem
    {
        public int Id { get; set; }

        public string Texto { get; set; }

        public ComboItem(int id, string texto)
        {
            Id = id;
            Texto = texto;
        }

        public override string ToString()
        {
            return Texto;
        }
    }
}
