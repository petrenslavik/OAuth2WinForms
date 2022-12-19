namespace AuthApp.Authorization
{
    public class AccessToken
    {
        private readonly string _value;
        private readonly DateTimeOffset _expirationDate;

        public AccessToken(string value, DateTimeOffset expirationDate)
        {
            _value = value;
            _expirationDate = expirationDate;
        }

        public bool IsValid => DateTimeOffset.UtcNow < _expirationDate;

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
