import csv

list_strings = []
list_strings.append("\0")

with open("../table.csv") as csvfile:
    reader = csv.reader(csvfile)
    for row in reader:
        list_strings.append(row[1])

curr_index = 0
list_offsets = []

# There are things I defined weirdly
list_strings[53] = " "
list_strings[54] = "\n"
list_strings[55] = "\t"
list_strings[75] = ","
list_strings[80] = '"'

with open("../MayaBinTable.Common/ENTRIES.txt",
          "w") as entry_file:  # The overwrite is wanted: This is a file that should change every build
    for entry in list_strings:
        list_offsets.append(curr_index)
        
        for ch in entry:
            entry_file.write(ch)
            curr_index = curr_index + 1
            
        entry_file.write("\0")
        curr_index = curr_index + 1

with open("../MayaBinTable.Common/OFFSETS.bin", "wb")  as offset_file:
    for offset in list_offsets:
        offset_file.write(offset.to_bytes(2, signed="false"))