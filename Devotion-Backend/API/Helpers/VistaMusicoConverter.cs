using System.Text.RegularExpressions;

namespace API.Helpers
{
    public static class VistaMusicoConverter
    {
        // Acordes válidos: C, Am, F#7, Bb, Gsus4, etc.
        private static readonly Regex AcordeRegex =
            new(@"\b[A-G](#|b)?(m|maj7|dim|aug|sus2|sus4|7)?\b",
                RegexOptions.Compiled);

        public static string Convertir(string texto)
        {
            var lineas = texto.Split('\n');
            var resultado = new List<string>();

            for (int i = 0; i < lineas.Length; i++)
            {
                var linea = lineas[i].TrimEnd();

                // Mantener encabezados y comentarios
                if (string.IsNullOrWhiteSpace(linea) ||
                    linea.StartsWith("//") ||
                    linea.StartsWith("Tono:", StringComparison.OrdinalIgnoreCase))
                {
                    resultado.Add(linea);
                    continue;
                }

                // Línea de acordes + línea de letra
                if (AcordeRegex.IsMatch(linea) && i + 1 < lineas.Length)
                {
                    var lineaAcordes = linea;
                    var lineaLetra = lineas[++i];

                    var letra = lineaLetra.ToCharArray().ToList();
                    var matches = AcordeRegex.Matches(lineaAcordes);

                    int offset = 0;

                    foreach (Match match in matches)
                    {
                        var acorde = $"[{match.Value}]";
                        int posicion = match.Index + offset;

                        if (posicion < letra.Count)
                        {
                            letra.InsertRange(posicion, acorde);
                            offset += acorde.Length;
                        }
                        else
                        {
                            letra.AddRange(acorde);
                        }
                    }

                    resultado.Add(new string(letra.ToArray()).Trim());
                }
                else
                {
                    resultado.Add(linea);
                }
            }

            return string.Join("\n", resultado);
        }
    }
}

