namespace SFB
{
    public readonly struct ExtensionFilter
    {
        public readonly string name;
        public readonly string[] extensions;

        public ExtensionFilter(string filterName, params string[] filterExtensions)
        {
            name = filterName;
            extensions = filterExtensions;
        }
    }
}