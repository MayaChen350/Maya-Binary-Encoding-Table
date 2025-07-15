using MayaBinTable.Common;

namespace MayaBinTable.Encoding;

public class Encoder
{
    public void Encode(StreamReader reader, FileStream writer)
    {
        LinkedList<char> baseString = new LinkedList<char>(); // String going to be encoded
        string currString = ""; // Current string that is being encoded
        string lastMatch = ""; // Last match found saved 

        // Write bytes as much as the text has text
        while (!reader.EndOfStream)
        {
            // Add 1000 characters or fewer to encode
            for (int i = 0; i < 1000 && !reader.EndOfStream; i++)
            {
                baseString.AddLast((char)reader.Read());
            }

            WriteResultEncoding(ref baseString, ref currString, ref lastMatch, writer);
        }

        // Write the rest of the characters
        WriteResultEncoding(ref baseString, ref currString, ref lastMatch, writer, true);
    }

    public void WriteResultEncoding(ref LinkedList<char> baseString, ref string currString, ref string lastMatch,
        FileStream writer, bool terminate = false)
    {
        List<string> bannedMatches = new List<string>();
        // Write as long as the baseString is not empty
        while (baseString.Any())
        {
            // Add a new char for the matching
            currString += baseString.First();
            baseString.RemoveFirst();

            // Check if there are any matches
            // If there are banned matches, don't consider them as matches
            if (MayaTable.HasMatchesWithFilter(currString, bannedMatches))
            {
                // If there is matches check if there is an exact match
                if (MayaTable.HasExactMatch(currString))
                    lastMatch = currString; // Save as the last match found

                // If there is nothing left in the baseString and terminating is asked
                if (baseString.Count == 0 && terminate)
                {
                    // Then if it is an exact match with smth
                    if (MayaTable.HasExactMatch(lastMatch))
                    {
                        // Then write the bytes
                        var bytes = MayaTable.GetBytesFromExactString(currString).AsSpan();
                        writer.Write(bytes);
                    }
                    // Otherwise
                    else
                    {
                        // Add the match to the banned match
                        bannedMatches.Add(lastMatch);
                        foreach (char chara in currString)
                        {
                            baseString.AddLast(chara);
                        }

                        lastMatch = "";
                        currString = "";
                    }
                }
            }
            // Otherwise if there is no match, check if there was a match before
            // If there is no match, but the current string has only one character, still works with the unknown character
            else if (lastMatch != "" || (currString.Length == 1 && lastMatch == ""))
            {
                // If there was an unknown character, write it as last match
                if (currString.Length == 1 && lastMatch == "")
                    lastMatch = currString;

                // Write down the byte from the last match
                var bytes = MayaTable.GetBytesFromExactString(lastMatch).AsSpan();
                writer.Write(bytes);
                // Substract the last match from the current string
                currString = currString.Substring(lastMatch.Length);
                // Remake the Queue ir needed
                if (currString != "")
                {
                    foreach (char chara in currString.Reverse())
                        baseString.AddFirst(chara);
                    // Reset the current string
                    currString = "";
                }

                // Reset the last match
                lastMatch = "";

                //if (baseString.Count == 985)
                //	Console.WriteLine(baseString.Count);
                Console.WriteLine(baseString.Count);
            }
        }
    }
}