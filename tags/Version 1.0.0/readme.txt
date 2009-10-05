Supported Data Types:
http://msdn.microsoft.com/en-us/library/aa237850%28SQL.80%29.aspx

1.) nchar(n) (national character(n))
Fixed-length Unicode data with a maximum length of 255 characters.
Default length = 1
Storage size, in bytes, is two times the number of characters entered.

2.) nvarchar(n)
(national character varying(n))
Variable-length Unicode data with a length of 1 to 255 characters.
Default length = 1
Storage size, in bytes, is two times the number of characters entered.

3.) Ntext
Variable-length Unicode data with a maximum length of (2^30 - 2) / 2 (536,870,911) characters. Storage size, in bytes, is two times the number of characters entered.

4.) binary(n)
Fixed-length binary data with a maximum length of 510 bytes. Default length = 1

5.) Varbinary(n)
Variable-length binary data with a maximum length of 510 bytes. Default length = 1

6.) Image
Variable-length binary data with a maximum length of 2^30 ? 1 (1,073,741,823) bytes.

7.) Uniqueidentifier
A globally unique identifier (GUID). Storage size is 16 bytes.

8.) IDENTITY [(s, i)]
s (seed) = starting value
i (increment) = increment value
This is a property of a data column, not a distinct data type.
Only data columns of the integer data types can be used for identity columns. A table can have only one identity column. A seed and increment can be specified and the column cannot be updated.

9.) ROWGUIDCOL
This is a property of a data column, not a distinct data type. It is a column in a table that is defined using the uniqueidentifier data type. A table can only have one ROWGUIDCOL column. 