namespace calculator.Classes.Functions
{
    internal class BaseFunction
    {
        public string Input { get; set; }
        public virtual string GetInputDegree()
        {
            return "BaseFunctionDegree";
        }

        public virtual string GetInputRadian()
        {
            return "BaseFunctionRadian";
        }
    }
}
