using PurplePete.ConfluenceProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurplePete.ConfluenceProvider;
public interface IConfluenceClient
{
	Task<IEnumerable<Page>?> SearchForKeywords(IEnumerable<string> keywords);
}
