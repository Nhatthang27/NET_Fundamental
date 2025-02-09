using OidcServer.Models;

namespace OidcServer.Repository
{
    public class InMemoryCodeItemRepository : ICodeItemRepository
    {
        private readonly Dictionary<string, CodeItem> _codeItems = new Dictionary<string, CodeItem>();

        public CodeItem? FindByCode(string code)
        {
            return _codeItems.TryGetValue(code, out var codeItem) ? codeItem : null;
        }

        public void Add(string code, CodeItem codeItem)
        {
            _codeItems[code] = codeItem;
        }

        public void Delete(CodeItem codeItem)
        {
            var key = _codeItems.FirstOrDefault(x => x.Value == codeItem).Key;
            if (key != null)
            {
                _codeItems.Remove(key);
            }
        }

    }
}
