using System.Text;

namespace HelperMethods;

public static class JsonConverterForBoard
{

    public static T[][] ToJaggedArray<T>(T[,] twoDimensionalArray)
    {
        int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
        int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
        int numberOfRows = rowsLastIndex - rowsFirstIndex + 1;

        int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
        int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
        int numberOfColumns = columnsLastIndex - columnsFirstIndex + 1;

        T[][] jaggedArray = new T[numberOfRows][];
        for (int i = 0; i < numberOfRows; i++)
        {
            jaggedArray[i] = new T[numberOfColumns];

            for (int j = 0; j < numberOfColumns; j++)
            {
                jaggedArray[i][j] = twoDimensionalArray[i + rowsFirstIndex, j + columnsFirstIndex];
            }
        }

        return jaggedArray;
    }

    public static T[,] To2D<T>(T[][] source)
    {
        int firstDim = source.Length;
        int secondDim =
            source.GroupBy(row => row.Length).Single()
                .Key; // throws InvalidOperationException if source is not rectangular

        var result = new T[firstDim, secondDim];
        for (int i = 0; i < firstDim; ++i)
        for (int j = 0; j < secondDim; ++j)
            result[i, j] = source[i][j];

        return result;
    }

    public static string IdGenerator()
    {
        StringBuilder builder = new StringBuilder();
        Enumerable
            .Range(1, 9)
            .Select(e => ((int)e).ToString())
            .Concat(Enumerable.Range(1, 9).Select(e => ((int)e).ToString()))
            .Concat(Enumerable.Range(0, 9).Select(e => e.ToString()))
            .OrderBy(e => Guid.NewGuid())
            .Take(3)
            .ToList().ForEach(e => builder.Append(e));
        string id = builder.ToString();
        return id;
    }
}