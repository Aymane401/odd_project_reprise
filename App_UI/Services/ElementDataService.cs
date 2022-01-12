using App_UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace App_UI.Services
{
    public class ElementDataService : IDataService<Element>
    {
        List<Element> data;
        public string Filename { get; set; }

        /// <summary>
        /// Implémentation d'un Singleton avec un type Lazy
        /// Le type Lazy permet de charger la classe seulement au premier
        /// appel d'Instance. Cela permet d'économiser de la mémoire
        /// Remarquez que le constructeur est privé.
        /// ATTENTION! Le Lazy Loading n'est pas matière à l'examen.
        /// </summary>
        private static readonly Lazy<ElementDataService> lazy = new Lazy<ElementDataService>(() => new ElementDataService());
        public static ElementDataService Instance { get => lazy.Value; }

        private ElementDataService()
        {
            /// Seulement si c'est une liste fixe!
            /// Autres méthodes si les données viennent de fichier 
            /// ou de base de données

            populate();
        }

        /// <summary>
        /// Populate the data member with 100 random records
        /// </summary>
        private void populate()
        {
            data = new List<Element>
            {
                new Element {AtomicNumber=1, Name="Hydrogen", Symbol="H", AtomicWeight=1.00794, Phase="Gas", Type="Non Metal", MeltingPoint=-259.14, BoilingPoint=-252.87, Density=0.0899, Discoverer="Cavendish, Henry", Discovery=1766},
                new Element {AtomicNumber=2, Name="Helium", Symbol="He", AtomicWeight=4.002602, Phase="Gas", Type="Noble Gas", MeltingPoint=-999, BoilingPoint=-268.93, Density=0.1785, Discoverer="Ramsey, Sir William & Cleve, Per Teodor", Discovery=1895},
                new Element {AtomicNumber=3, Name="Lithium", Symbol="Li", AtomicWeight=6.941, Phase="Solid", Type="Alkali Metal", MeltingPoint=180.54, BoilingPoint=1342, Density=0.535, Discoverer="Arfvedson, Johan August", Discovery=1817},
                new Element {AtomicNumber=4, Name="Beryllium", Symbol="Be", AtomicWeight=9.012182, Phase="Solid", Type="Alkaline Earth Metal", MeltingPoint=1287, BoilingPoint=2470, Density=1.848, Discoverer="Vauquelin, Nicholas Louis", Discovery=1797},
                new Element {AtomicNumber=5, Name="Boron", Symbol="B", AtomicWeight=10.811, Phase="Solid", Type="Metalloids", MeltingPoint=2075, BoilingPoint=4000, Density=2.46, Discoverer="Davy, Sir Humphry & Thénard, Louis-Jaques  & Gay-Lussac, Louis-Joseph", Discovery=1808},
                new Element {AtomicNumber=6, Name="Carbon", Symbol="C", AtomicWeight=12.0107, Phase="Solid", Type="Non Metal", MeltingPoint=3550, BoilingPoint=4027, Density=2.26, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=7, Name="Nitrogen", Symbol="N", AtomicWeight=14.0067, Phase="Gas", Type="Non Metal", MeltingPoint=-210.1, BoilingPoint=-195.79, Density=1.251, Discoverer="Rutherford, Daniel", Discovery=1772},
                new Element {AtomicNumber=8, Name="Oxygen", Symbol="O", AtomicWeight=15.9994, Phase="Gas", Type="Non Metal", MeltingPoint=-218.3, BoilingPoint=-182.9, Density=1.429, Discoverer="Priestley, Joseph & Scheele, Carl Wilhelm", Discovery=1774},
                new Element {AtomicNumber=9, Name="Fluorine", Symbol="F", AtomicWeight=18.9984032, Phase="Gas", Type="Halogen", MeltingPoint=-219.6, BoilingPoint=-188.12, Density=1.696, Discoverer="Moissan, Henri", Discovery=1886},
                new Element {AtomicNumber=10, Name="Neon", Symbol="Ne", AtomicWeight=20.1797, Phase="Gas", Type="Noble Gas", MeltingPoint=-248.59, BoilingPoint=-246.08, Density=0.9, Discoverer="Ramsay, William & Travers, Morris", Discovery=1898},
                new Element {AtomicNumber=11, Name="Sodium", Symbol="Na", AtomicWeight=22.98977, Phase="Solid", Type="Alkali Metal", MeltingPoint=97.72, BoilingPoint=883, Density=0.968, Discoverer="Davy, Sir Humphry", Discovery=1807},
                new Element {AtomicNumber=12, Name="Magnesium", Symbol="Mg", AtomicWeight=24.305, Phase="Solid", Type="Alkaline Earth Metal", MeltingPoint=650, BoilingPoint=1090, Density=1.738, Discoverer="Black, Joseph", Discovery=1755},
                new Element {AtomicNumber=13, Name="Aluminum", Symbol="Al", AtomicWeight=26.981538, Phase="Solid", Type="Poor Metal", MeltingPoint=660.32, BoilingPoint=2519, Density=2.7, Discoverer="Oersted, Hans Christian", Discovery=1825},
                new Element {AtomicNumber=14, Name="Silicon", Symbol="Si", AtomicWeight=28.0855, Phase="Solid", Type="Metalloids", MeltingPoint=1414, BoilingPoint=2900, Density=2.33, Discoverer="Berzelius, Jöns Jacob", Discovery=1824},
                new Element {AtomicNumber=15, Name="Phosphorus", Symbol="P", AtomicWeight=30.97361, Phase="Solid", Type="Non Metal", MeltingPoint=44.2, BoilingPoint=280.5, Density=1.823, Discoverer="Brandt, Hennig", Discovery=1669},
                new Element {AtomicNumber=16, Name="Sulfur", Symbol="S", AtomicWeight=32.065, Phase="Solid", Type="Non Metal", MeltingPoint=115.21, BoilingPoint=444.72, Density=1.96, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=17, Name="Chlorine", Symbol="Cl", AtomicWeight=35.453, Phase="Gas", Type="Halogen", MeltingPoint=-101.5, BoilingPoint=-34.04, Density=3.214, Discoverer="Scheele, Carl Wilhelm", Discovery=1774},
                new Element {AtomicNumber=18, Name="Argon", Symbol="Ar", AtomicWeight=39.948, Phase="Gas", Type="Noble Gas", MeltingPoint=-189.3, BoilingPoint=-185.8, Density=1.784, Discoverer="Ramsay, Sir William & Strutt, John (Lord Rayleigh)", Discovery=1894},
                new Element {AtomicNumber=19, Name="Potassium", Symbol="K", AtomicWeight=39.0983, Phase="Solid", Type="Alkali Metal", MeltingPoint=63.38, BoilingPoint=759, Density=0.856, Discoverer="Davy, Sir Humphry", Discovery=1807},
                new Element {AtomicNumber=20, Name="Calcium", Symbol="Ca", AtomicWeight=40.078, Phase="Solid", Type="Alkaline Earth Metal", MeltingPoint=842, BoilingPoint=1484, Density=1.55, Discoverer="Davy, Sir Humphry", Discovery=1808},
                new Element {AtomicNumber=21, Name="Scandium", Symbol="Sc", AtomicWeight=44.95591, Phase="Solid", Type="Transition Metal", MeltingPoint=1541, BoilingPoint=2830, Density=2.985, Discoverer="Nilson, Lars Fredrik", Discovery=1879},
                new Element {AtomicNumber=22, Name="Titanium", Symbol="Ti", AtomicWeight=47.867, Phase="Solid", Type="Transition Metal", MeltingPoint=1668, BoilingPoint=3287, Density=4.507, Discoverer="Gregor, William", Discovery=1791},
                new Element {AtomicNumber=23, Name="Vanadium", Symbol="V", AtomicWeight=50.9415, Phase="Solid", Type="Transition Metal", MeltingPoint=1910, BoilingPoint=3407, Density=6.11, Discoverer="Del Rio, Andrés Manuel (1801) & Sefström, Nils Gabriel (1830)", Discovery=1801},
                new Element {AtomicNumber=24, Name="Chromium", Symbol="Cr", AtomicWeight=51.9961, Phase="Solid", Type="Transition Metal", MeltingPoint=1907, BoilingPoint=2671, Density=7.14, Discoverer="Vauquelin", Discovery=1797},
                new Element {AtomicNumber=25, Name="Manganese", Symbol="Mn", AtomicWeight=54.938049, Phase="Solid", Type="Transition Metal", MeltingPoint=1246, BoilingPoint=2061, Density=7.47, Discoverer="Gahn, Johan Gottlieb", Discovery=1774},
                new Element {AtomicNumber=26, Name="Iron", Symbol="Fe", AtomicWeight=55.845, Phase="Solid", Type="Transition Metal", MeltingPoint=1538, BoilingPoint=2861, Density=7.874, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=27, Name="Cobalt", Symbol="Co", AtomicWeight=58.9332, Phase="Solid", Type="Transition Metal", MeltingPoint=1495, BoilingPoint=2927, Density=8.9, Discoverer="Brandt, Georg", Discovery=1735},
                new Element {AtomicNumber=28, Name="Nickel", Symbol="Ni", AtomicWeight=58.6934, Phase="Solid", Type="Transition Metal", MeltingPoint=1455, BoilingPoint=2913, Density=8.908, Discoverer="Cronstedt, Alex Fredrik", Discovery=1751},
                new Element {AtomicNumber=29, Name="Copper", Symbol="Cu", AtomicWeight=63.546, Phase="Solid", Type="Transition Metal", MeltingPoint=1084.62, BoilingPoint=2927, Density=8.92, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=30, Name="Zinc", Symbol="Zn", AtomicWeight=65.409, Phase="Solid", Type="Transition Metal", MeltingPoint=419.53, BoilingPoint=907, Density=7.14, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=31, Name="Gallium", Symbol="Ga", AtomicWeight=69.723, Phase="Solid", Type="Poor Metal", MeltingPoint=29.76, BoilingPoint=2204, Density=5.904, Discoverer="Lecoq de Boisbaudran, Paul-Émile", Discovery=1875},
                new Element {AtomicNumber=32, Name="Germanium", Symbol="Ge", AtomicWeight=72.64, Phase="Solid", Type="Metalloids", MeltingPoint=938.3, BoilingPoint=2820, Density=5.323, Discoverer="Winkler, Clemens A.", Discovery=1886},
                new Element {AtomicNumber=33, Name="Arsenic", Symbol="As", AtomicWeight=74.9216, Phase="Solid", Type="Metalloids", MeltingPoint=817, BoilingPoint=614, Density=5.727, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=34, Name="Selenium", Symbol="Se", AtomicWeight=78.96, Phase="Solid", Type="Non Metal", MeltingPoint=221, BoilingPoint=685, Density=4.819, Discoverer="Berzelius, Jöns Jacob", Discovery=1817},
                new Element {AtomicNumber=35, Name="Bromine", Symbol="Br", AtomicWeight=79.904, Phase="Liquid", Type="Halogen", MeltingPoint=-7.3, BoilingPoint=59, Density=3.12, Discoverer="Balard, Antoine-Jérôme", Discovery=1826},
                new Element {AtomicNumber=36, Name="Krypton", Symbol="Kr", AtomicWeight=83.798, Phase="Gas", Type="Noble Gas", MeltingPoint=-157.36, BoilingPoint=-153.22, Density=3.75, Discoverer="Ramsay, Sir William & Travers, Morris", Discovery=1898},
                new Element {AtomicNumber=37, Name="Rubidium", Symbol="Rb", AtomicWeight=85.4678, Phase="Solid", Type="Alkali Metal", MeltingPoint=39.31, BoilingPoint=688, Density=1.532, Discoverer="Bunsen, Robert Wilhelm & Kirchhoff, Gustav Robert", Discovery=1861},
                new Element {AtomicNumber=38, Name="Strontium", Symbol="Sr", AtomicWeight=87.62, Phase="Solid", Type="Alkaline Earth Metal", MeltingPoint=777, BoilingPoint=1382, Density=2.63, Discoverer="Crawford, Adair", Discovery=1790},
                new Element {AtomicNumber=39, Name="Yttrium", Symbol="Y", AtomicWeight=88.90585, Phase="Solid", Type="Transition Metal", MeltingPoint=1526, BoilingPoint=3345, Density=4.472, Discoverer="Gadolin, Johan", Discovery=1789},
                new Element {AtomicNumber=40, Name="Zirconium", Symbol="Zr", AtomicWeight=91.224, Phase="Solid", Type="Transition Metal", MeltingPoint=1855, BoilingPoint=4409, Density=6.511, Discoverer="Klaproth, Martin Heinrich", Discovery=1789},
                new Element {AtomicNumber=41, Name="Niobium", Symbol="Nb", AtomicWeight=92.90638, Phase="Solid", Type="Transition Metal", MeltingPoint=2477, BoilingPoint=4744, Density=8.57, Discoverer="Hatchet, Charles", Discovery=1801},
                new Element {AtomicNumber=42, Name="Molybdenum", Symbol="Mo", AtomicWeight=95.94, Phase="Solid", Type="Transition Metal", MeltingPoint=2623, BoilingPoint=4639, Density=10.28, Discoverer="Scheele, Carl Welhelm", Discovery=1778},
                new Element {AtomicNumber=43, Name="Technetium", Symbol="Tc", AtomicWeight=98, Phase="Synthetic", Type="Transition Metal", MeltingPoint=2157, BoilingPoint=4265, Density=11.5, Discoverer="Perrier, Carlo & Segrè, Emilio", Discovery=1937},
                new Element {AtomicNumber=44, Name="Ruthenium", Symbol="Ru", AtomicWeight=101.07, Phase="Solid", Type="Transition Metal", MeltingPoint=2334, BoilingPoint=4150, Density=12.37, Discoverer="Klaus, Karl Karlovich", Discovery=1844},
                new Element {AtomicNumber=45, Name="Rhodium", Symbol="Rh", AtomicWeight=102.9055, Phase="Solid", Type="Transition Metal", MeltingPoint=1964, BoilingPoint=3695, Density=12.45, Discoverer="Wollaston, William Hyde", Discovery=1803},
                new Element {AtomicNumber=46, Name="Palladium", Symbol="Pd", AtomicWeight=106.42, Phase="Solid", Type="Transition Metal", MeltingPoint=1554.9, BoilingPoint=2963, Density=12.023, Discoverer="Wollaston, William Hyde", Discovery=1803},
                new Element {AtomicNumber=47, Name="Silver", Symbol="Ag", AtomicWeight=107.8682, Phase="Solid", Type="Transition Metal", MeltingPoint=961.78, BoilingPoint=2162, Density=10.49, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=48, Name="Cadmium", Symbol="Cd", AtomicWeight=112.411, Phase="Solid", Type="Transition Metal", MeltingPoint=321.07, BoilingPoint=767, Density=8.65, Discoverer="Stromeyer, Prof. Friedrich", Discovery=1817},
                new Element {AtomicNumber=49, Name="Indium", Symbol="In", AtomicWeight=114.818, Phase="Solid", Type="Poor Metal", MeltingPoint=156.6, BoilingPoint=2072, Density=7.31, Discoverer="Reich, Ferdinand & Richter, Hieronymus", Discovery=1863},
                new Element {AtomicNumber=50, Name="Tin", Symbol="Sn", AtomicWeight=118.71, Phase="Solid", Type="Poor Metal", MeltingPoint=231.93, BoilingPoint=2602, Density=7.31, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=51, Name="Antimony", Symbol="Sb", AtomicWeight=121.76, Phase="Solid", Type="Metalloids", MeltingPoint=630.63, BoilingPoint=1587, Density=6.697, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=52, Name="Tellurium", Symbol="Te", AtomicWeight=127.6, Phase="Solid", Type="Metalloids", MeltingPoint=449.51, BoilingPoint=988, Density=6.24, Discoverer="Müller von Reichenstein, Franz Joseph", Discovery=1782},
                new Element {AtomicNumber=53, Name="Iodine", Symbol="I", AtomicWeight=126.90447, Phase="Solid", Type="Halogen", MeltingPoint=113.7, BoilingPoint=184.3, Density=4.94, Discoverer="Courtois, Bernard", Discovery=1811},
                new Element {AtomicNumber=54, Name="Xenon", Symbol="Xe", AtomicWeight=131.293, Phase="Gas", Type="Noble Gas", MeltingPoint=-111.8, BoilingPoint=-108, Density=5.9, Discoverer="Ramsay, William & Travers, Morris William", Discovery=1898},
                new Element {AtomicNumber=55, Name="Cesium", Symbol="Cs", AtomicWeight=132.90545, Phase="Solid", Type="Alkali Metal", MeltingPoint=28.44, BoilingPoint=671, Density=1.879, Discoverer="Kirchhoff, Gustav & Bunsen, Robert", Discovery=1860},
                new Element {AtomicNumber=56, Name="Barium", Symbol="Ba", AtomicWeight=137.327, Phase="Solid", Type="Alkaline Earth Metal", MeltingPoint=727, BoilingPoint=1870, Density=3.51, Discoverer="Davy, Sir Humphry", Discovery=1808},
                new Element {AtomicNumber=57, Name="Lanthanum", Symbol="La", AtomicWeight=138.9055, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=920, BoilingPoint=3464, Density=6.146, Discoverer="Mosander, Carl Gustav", Discovery=1839},
                new Element {AtomicNumber=58, Name="Cerium", Symbol="Ce", AtomicWeight=140.116, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=798, BoilingPoint=3360, Density=6.689, Discoverer="Hisinger, Wilhelm & Berzelius, Jöns Jacob/Klaproth, Martin Heinrich", Discovery=1803},
                new Element {AtomicNumber=59, Name="Praseodymium", Symbol="Pr", AtomicWeight=140.90765, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=931, BoilingPoint=3290, Density=6.64, Discoverer="Von Welsbach, Baron Auer", Discovery=1885},
                new Element {AtomicNumber=60, Name="Neodymium", Symbol="Nd", AtomicWeight=144.24, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1021, BoilingPoint=3100, Density=7.01, Discoverer="Von Welsbach, Baron Auer", Discovery=1885},
                new Element {AtomicNumber=61, Name="Promethium", Symbol="Pm", AtomicWeight=145, Phase="Synthetic", Type="Rare Earth Metal", MeltingPoint=1100, BoilingPoint=3000, Density=7.264, Discoverer="Marinsky, Jacob A. & Coryell, Charles D. & Glendenin, Lawerence. E.", Discovery=1944},
                new Element {AtomicNumber=62, Name="Samarium", Symbol="Sm", AtomicWeight=150.36, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1072, BoilingPoint=1803, Density=7.353, Discoverer="Lecoq de Boisbaudran, Paul-Émile", Discovery=1879},
                new Element {AtomicNumber=63, Name="Europium", Symbol="Eu", AtomicWeight=151.964, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=822, BoilingPoint=1527, Density=5.244, Discoverer="Demarçay, Eugène-Antole", Discovery=1901},
                new Element {AtomicNumber=64, Name="Gadolinium", Symbol="Gd", AtomicWeight=157.25, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1313, BoilingPoint=3250, Density=7.901, Discoverer="De Marignac, Charles Galissard", Discovery=1880},
                new Element {AtomicNumber=65, Name="Terbium", Symbol="Tb", AtomicWeight=158.92534, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1356, BoilingPoint=3230, Density=8.219, Discoverer="Mosander, Carl Gustav", Discovery=1843},
                new Element {AtomicNumber=66, Name="Dysprosium", Symbol="Dy", AtomicWeight=162.5, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1412, BoilingPoint=2567, Density=8.551, Discoverer="Lecoq de Boisbaudran, Paul-Émile", Discovery=1886},
                new Element {AtomicNumber=67, Name="Holmium", Symbol="Ho", AtomicWeight=164.93032, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1474, BoilingPoint=2700, Density=8.795, Discoverer="Cleve, Per Theodor", Discovery=1879},
                new Element {AtomicNumber=68, Name="Erbium", Symbol="Er", AtomicWeight=167.259, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1497, BoilingPoint=2868, Density=9.066, Discoverer="Mosander, Carl Gustav", Discovery=1842},
                new Element {AtomicNumber=69, Name="Thulium", Symbol="Tm", AtomicWeight=168.93421, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1545, BoilingPoint=1950, Density=9.321, Discoverer="Cleve, Per Teodor", Discovery=1879},
                new Element {AtomicNumber=70, Name="Ytterbium", Symbol="Yb", AtomicWeight=173.04, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=819, BoilingPoint=1196, Density=6.57, Discoverer="De Marignac, Jean Charles Galissard", Discovery=1878},
                new Element {AtomicNumber=71, Name="Lutetium", Symbol="Lu", AtomicWeight=174.967, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1663, BoilingPoint=3402, Density=9.841, Discoverer="Urbain, Georges", Discovery=1907},
                new Element {AtomicNumber=72, Name="Hafnium", Symbol="Hf", AtomicWeight=178.49, Phase="Solid", Type="Transition Metal", MeltingPoint=2233, BoilingPoint=4603, Density=13.31, Discoverer="Coster, Dirk & De Hevesy, George Charles", Discovery=1923},
                new Element {AtomicNumber=73, Name="Tantalum", Symbol="Ta", AtomicWeight=180.9479, Phase="Solid", Type="Transition Metal", MeltingPoint=3017, BoilingPoint=5458, Density=16.65, Discoverer="Ekeberg, Anders Gustav", Discovery=1802},
                new Element {AtomicNumber=74, Name="Tungsten", Symbol="W", AtomicWeight=183.84, Phase="Solid", Type="Transition Metal", MeltingPoint=3422, BoilingPoint=5555, Density=19.25, Discoverer="Elhuyar, Juan José & Elhuyar, Fausto", Discovery=1783},
                new Element {AtomicNumber=75, Name="Rhenium", Symbol="Re", AtomicWeight=186.207, Phase="Solid", Type="Transition Metal", MeltingPoint=3186, BoilingPoint=5596, Density=21.02, Discoverer="Noddack, Walter & Berg, Otto Carl & Tacke, Ida", Discovery=1925},
                new Element {AtomicNumber=76, Name="Osmium", Symbol="Os", AtomicWeight=190.23, Phase="Solid", Type="Transition Metal", MeltingPoint=3033, BoilingPoint=5012, Density=22.61, Discoverer="Tennant, Smithson", Discovery=1803},
                new Element {AtomicNumber=77, Name="Iridium", Symbol="Ir", AtomicWeight=192.217, Phase="Solid", Type="Transition Metal", MeltingPoint=2466, BoilingPoint=4428, Density=22.65, Discoverer="Tennant, Smithson", Discovery=1803},
                new Element {AtomicNumber=78, Name="Platinum", Symbol="Pt", AtomicWeight=195.078, Phase="Solid", Type="Transition Metal", MeltingPoint=1768.3, BoilingPoint=3825, Density=21.09, Discoverer="Ulloa, Antonio de", Discovery=1735},
                new Element {AtomicNumber=79, Name="Gold", Symbol="Au", AtomicWeight=196.96655, Phase="Solid", Type="Transition Metal", MeltingPoint=1064.18, BoilingPoint=2856, Density=19.3, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=80, Name="Mercury", Symbol="Hg", AtomicWeight=200.59, Phase="Liquid", Type="Transition Metal", MeltingPoint=-38.83, BoilingPoint=356.73, Density=13.534, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=81, Name="Thallium", Symbol="Tl", AtomicWeight=204.3833, Phase="Solid", Type="Poor Metal", MeltingPoint=304, BoilingPoint=1473, Density=11.85, Discoverer="Crookes, William", Discovery=1861},
                new Element {AtomicNumber=82, Name="Lead", Symbol="Pb", AtomicWeight=207.2, Phase="Solid", Type="Poor Metal", MeltingPoint=327.46, BoilingPoint=1749, Density=11.34, Discoverer="unknown", Discovery=0},
                new Element {AtomicNumber=83, Name="Bismuth", Symbol="Bi", AtomicWeight=208.98038, Phase="Solid", Type="Poor Metal", MeltingPoint=271.3, BoilingPoint=1564, Density=9.78, Discoverer="Geoffroy, Claude ", Discovery=0},
                new Element {AtomicNumber=84, Name="Polonium", Symbol="Po", AtomicWeight=209, Phase="Solid", Type="Metalloids ?", MeltingPoint=254, BoilingPoint=962, Density=9.196, Discoverer="Curie, Marie & Pierre", Discovery=1898},
                new Element {AtomicNumber=85, Name="Astatine", Symbol="At", AtomicWeight=210, Phase="Solid", Type="Metalloids", MeltingPoint=302, BoilingPoint=9999, Density=9999, Discoverer="Corson, Dale R. & Mackenzie, K. R.", Discovery=1940},
                new Element {AtomicNumber=86, Name="Radon", Symbol="Rn", AtomicWeight=222, Phase="Gas", Type="Noble Gas", MeltingPoint=-71, BoilingPoint=-61.7, Density=9.73, Discoverer="Dorn, Friedrich Ernst", Discovery=1900},
                new Element {AtomicNumber=87, Name="Francium", Symbol="Fr", AtomicWeight=223, Phase="Solid", Type="Alkali Metal", MeltingPoint=-999, BoilingPoint=9999, Density=9999, Discoverer="Perey, Marguerite", Discovery=1939},
                new Element {AtomicNumber=88, Name="Radium", Symbol="Ra", AtomicWeight=226, Phase="Solid", Type="Alkaline Earth Metal", MeltingPoint=700, BoilingPoint=1737, Density=5, Discoverer="Curie, Marie & Pierre", Discovery=1898},
                new Element {AtomicNumber=89, Name="Actinium", Symbol="Ac", AtomicWeight=227, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1050, BoilingPoint=3200, Density=10.07, Discoverer="Debierne, André", Discovery=1899},
                new Element {AtomicNumber=90, Name="Thorium", Symbol="Th", AtomicWeight=232.0381, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1750, BoilingPoint=4820, Density=11.724, Discoverer="Berzelius, Jöns Jacob", Discovery=1829},
                new Element {AtomicNumber=91, Name="Protactinium", Symbol="Pa", AtomicWeight=231.03588, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1572, BoilingPoint=4000, Density=15.37, Discoverer="Göhring, Otto & Fajans, Kasimir", Discovery=1913},
                new Element {AtomicNumber=92, Name="Uranium", Symbol="U", AtomicWeight=238.02891, Phase="Solid", Type="Rare Earth Metal", MeltingPoint=1135, BoilingPoint=3927, Density=19.05, Discoverer="Klaproth, Martin Heinrich", Discovery=1789},

            };



        }

        public int Delete(Element value)
        {
            int result = 0;

            if (data.Contains(value))
            {
                data.Remove(value);
                result = 1;
            }

            return result;
        }

        public IEnumerable<Element> GetAll()
        {
            foreach (var a in data)
            {
                yield return a;
            }
        }

        public Element GetById(int id)
        {
            return data.Find(p => p.Id == id);
        }

        public int SetAllFromJson(string allContent)
        {
            int result;

            var people = JsonConvert.DeserializeObject<List<Element>>(allContent);

            result = people.Count;

            data = people;

            return result;
        }

        public string GetAllAsJson()
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        public int Insert(Element value)
        {

            int result = 0;

            try
            {
                data.Add(value);
                result = data.Count;
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        public int Update(Element value)
        {

            if (value == null) return 0;

            var p = GetById(value.Id);

            if (p == null)
            {
                return Insert(value);
            }


            Type type = p.GetType();

            /// Cette boucle permet de parcourir les propriétés de l'objet
            /// et ensuite de copier ce qui n'est pas égale.
            foreach (PropertyInfo info in type.GetProperties(
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.Instance |
                        BindingFlags.GetProperty))
            {
                if (!info.CanWrite) continue;

                var oValue = info.GetValue(p, null);
                var nValue = info.GetValue(value, null);

                if (oValue != nValue)
                {
                    info.SetValue(p, info.GetValue(value, null));
                }
            }

            return 1;
        }
    }
}
