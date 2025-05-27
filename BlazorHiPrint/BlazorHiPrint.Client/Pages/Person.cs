namespace BlazorHiPrint.Client.Pages
{
    public class Person
    {
        public Person() { }
        public Person(int personId, string name, DateOnly promotionDate)
        {
            PersonId = personId;
            Name = name;
            PromotionDate = promotionDate;
        }

        public int PersonId { get; set; }
        public string Name { get; set; }
        public DateOnly PromotionDate { get; set; }
    }
}
