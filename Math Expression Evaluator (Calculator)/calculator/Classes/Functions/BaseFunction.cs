namespace calculator.Classes.Functions
{
    internal class BaseFunction
    {
        public string Input { get; set; }
        public virtual string GetInputDegree() => "BaseFunctionDegree";

        public virtual string GetInputRadian() => "BaseFunctionRadian";
    }
}
