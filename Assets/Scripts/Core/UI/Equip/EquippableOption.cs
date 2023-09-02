namespace MGCNTN.Core
{
    public class EquippableOption : Option
    {
        public Equippable equippable { get; private set; }

        public void changeOption(Equippable _equippable)
        {
            equippable = _equippable;
            if (_equippable != null)
                updateText();
            else
                textBox.text = string.Empty;
        }

        public override void clear()
        {
            base.clear();
            equippable = null;
        }
        /// Private
        protected override void updateText(string text = null) => textBox.text = equippable.Data.displayName;
    }
}