namespace RACI_Grid
{
    public class RaciDocument
    {
        // I considered a two dimensional array for storing the data, similar to
        // https://stackoverflow.com/questions/17121045/multidimensional-dictionary-with-add-new-value-if-key-not-in-dictionary

        // There really won't be that many values to store
        // I believe the array will be fairly sparse

        // Question: Can an indexer have multiple values?
        // I think so : https://social.msdn.microsoft.com/Forums/vstudio/en-US/c02abcfd-3e3e-484e-9a7c-d2dcb32123e5/how-to-invoke-multi-parameter-indexer-in-c-?forum=netfxbcl
        // Try It
        // Tried it.  It works

        public string this[string activity, string person]
        {
            set
            {
                RaciDataItem foundItem = FindItem(activity, person);
                if (foundItem != null)
                {
                    foundItem.RaciValue = value;
                }
                else
                {
                    RaciData.Add(new RaciDataItem() { ActivityName = activity, Person = person, RaciValue = value });
                }
            }

            get
            {
                RaciDataItem foundItem = FindItem(activity, person);
                if(foundItem != null)
                {
                    return foundItem.RaciValue;
                }
                return string.Empty;
            }
        }

        private RaciDataItem FindItem(string activity, string person)
        {
            // on average, we will only need to go half way through (on retrieval)
            // When adding, this will have to go all the way through the list, in order to verify
            RaciDataItem rtnVal = null;
            foreach (RaciDataItem item in RaciData)
            {
                if(item.ActivityName.Equals(activity, StringComparison.OrdinalIgnoreCase) &&
                    item.Person.Equals(person, StringComparison.OrdinalIgnoreCase)
                    )
                {
                    rtnVal = item;
                    break;
                }
            }

            return rtnVal;
        }

        public List<RaciDataItem> RaciData { get; set; } = new();
    }
}
