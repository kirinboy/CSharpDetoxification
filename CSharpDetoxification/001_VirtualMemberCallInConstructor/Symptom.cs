namespace CSharpDetoxification.VirtualMemberCallInConstructor
{
    public class Symptom
    {
        public Symptom()
        {
            Name = "Virtual member call in constructor";
            Cure();
        }

        public virtual string Name { get; set; }

        public virtual void Cure()
        {
        }
    }
}
