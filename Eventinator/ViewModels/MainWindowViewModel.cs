using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.X11;
using DynamicData.Kernel;
using Eventinator.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;

namespace Eventinator.ViewModels {
    public class MainWindowViewModel: ViewModelBase {
        private static readonly string img_a = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAMFBMVEUzMzP38/OUkpLDwMBiYWHe29tLS0usqqp4d3eFg4Pr5+fPy8tsa2uem5s9PDxUU1NbyWHjAAAAeklEQVR4nGMQBAGphWCKAcxuYAmEcyQZGE5BOcELlzAwpAruBnFEP8xaw8DwVYhvI5Aj5cC5nIFBS4E/EKRsP8Os+ty1DMlgPdIfOI2NFdgMIaaFMABBMtRoUQcGBpAExB4FBgaOhdTjsBxA4jAVUMaJaF15Gh4gMAAAi+YqvgABMVEAAAAASUVORK5CYII=";
        private static readonly string img_b = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAMFBMVEUzMzP38/OVk5PGw8NjYmLf29utq6t7enpLS0vr5+e6t7fSz8+hn5+Jh4dwbm5XV1eGTYI5AAAA9UlEQVR4nE2QP0vDUBTFf4QOIWDkGFJ5DQ7OTsHNP5DRIiIPxNV260fQLbMd5KE0myB+Abs51m8QRycXF3dBsYO+mCoe3h1+j3u55x70T/gyu19vGwswI7z2W7jmA/8eGkiL+T28joIGqtj1ytgm5dDD4NNqkiubjYUpmm6pIhAJPCqxOqHjWIG+qc91B1NW4emorRuqMHD+t58W5ZAqynUF67qsh6xFkp/LpTqn6zdnh+9OZm9KdnF866R0Zyt2qD5lLM14ibwd7zLwbuHMQxdC24Nl29wzYMllBfOf45IyPHgmtm0GkxI6+SIQmc1t95vOn74BVRI9CVo/M5YAAAAASUVORK5CYII=";
        private static readonly string img_c = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAMFBMVEUzMzP38/OUkpLIxcViYWGvrKx6eXlLS0ve29uIhoajoaFAQEC9u7vs6elYWFjSzs6859c1AAAAiElEQVR4nGMQRAIMuDmyh5SgYL0gwzUGOAhk+I7g5DJ8Z2m8ieDwCEoUwDn7BWUfMDCwOYA5XILCGxgY5l4Dcxj0wxkYOC/KJoA5IDBXUPAamMNuX8B5UVBQ4gGIUyz4HSgBNAbIYTcUlLgI43BCXYnJ4X4HBUDT+hAOTWQQ3QBjszTi8TZODgCqTzpaCI886gAAAABJRU5ErkJggg==";
        private static readonly string img_d = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAMFBMVEUzMzP38/OUkpLFwsJjYmLe29usqqp7enpLS0u5trbRzc1XV1fr6OiHhYWhn59AQEA/BtRUAAAAyElEQVR4nIXPoQrCcBDH8S8ibjBk/JUZ5I/MZBaroNi0zaoPYLTYDINhVxTMawaDL2BY0aKwBzD4AIaBL+BNEQyiF+74cPDjDvVR/IbTWG7f6CdAN3riDJ1aQi9FecdEqUPCTdAiEzlNGUZEMWGqKlxKUGUAC0FGt8nhYnrRBjOMsfCxTmOJrrvS2lhDgR2mmMu8QlbWBgVJOUJQ9pmhx9z1au8dMeso6aFSOiaQC/SV/FaPMLz0UCeWHKzF6wW99u1O+P/tr3gAO9gzpYRgvMcAAAAASUVORK5CYII=";
        private static readonly string img_e = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAMFBMVEUzMzP38/OUkpLHxMRjYmKsqqp7enpLS0ve29u7ubmHhYXq5uZXV1dAQEDSzs6ioKDGzH/RAAAAmklEQVR4nGMQFNwtKC0IAQyCklyCHwJhHLk7jROyYRzpBMUtrDCO4HNW4buGMI7kncaAbBhHOsGwpwLGEZzUKPobzoEDhm0MUMD2UJDhOYzDwIrg8BxgfwjnMEteUERwpA9kIzgSBxLhHM4uho9IprE0InE8EEazKQNd0APl3AM5RwrKAXmCQcIB4oCHYFdvzTlz5syR3xheAABN8S74282AVAAAAABJRU5ErkJggg==";
        private static readonly string img_f = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAMFBMVEUzMzP38/OWlJRlZGTDwMDe29tLS0t+fHysqqrRzc3r5+eJiIhycXG4tbVAQEBaWlqcvWeBAAAAgUlEQVR4nGMQhAMpQQY40/2BIowT9IGBoQrCkdzHwADliDYzMMA5DQxInA1gJhsS52zUBxjn9URBKSiHuRVkEZSTKUgeZ+XdBDhH9gHMBUDOPIhzgs58AHIcwBwJIAnkLGZg4HNkCINwpNT4TgqCOVZQ74KU8V2EcYAGdAnCOUgAADEONxbmMeY7AAAAAElFTkSuQmCC";
        private static readonly string img_g = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAMFBMVEX38/MzMzOUkpLHxMRoZ2dOTU2sqqrh3d19e3tAQEBcW1uhnp67ubnr6OjW09OHhYWRl8+UAAAAcklEQVR4nGNgYOC9CwEXGBgYeAIFIWAtkHMQyhaMAypyROLwBeLhyIDMegXlSBsDwVFsyoSBFjDcgnIklIDgJ0GjSeOsBXGCwV677qgA4iiA7GTgbUxgYGBf6A7lgAQ/wvT4AjklSKYx8KasgoANDCgAAA6aNVUFKsJ8AAAAAElFTkSuQmCC";
        private static readonly string img_h = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAMFBMVEUzMzP38/OUkpLDwMBgX1/c2dl7enqrqalJSUnq5uaIhoZwbm7Py8tUU1Ohn589PDy1K139AAAA5klEQVR4nGMQRAIMIGKm3D3JiVDOrMwChspdE8EcmQumFxiuGtwBc+xZ5jEwsIdwGAI5ohe4pjEwsM1iuArkSDGwxzIw8IYzcAE5QUqqQUpKSqFKSoEMKPZIbmC4WsDAwFB6gWEng+gFBs8FQI6mA8NVBtEGCGelA0MrKkfSgcFTAcipBNIMgkVKLw8BjT6qpFTIICg5c6LkzJkzBYGIQTCWgTmCgYEjmIEFaKk4A9NsBgbuSQzsIFcrABkgAUUQZxrHmQ8Mf05wTQT79L1mAkOm/kOIt0WfxvKGPoUFiKDkSUTowAAAeBg9riCawYYAAAAASUVORK5CYII=";
        private static readonly string img_i = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYBAMAAAASWSDLAAAAMFBMVEUzMzP38/OUkpLFwsJmZWXe29tLS0usqqp9e3tYWFjSzs7r5+eIhoZAQEBwbm6em5vGWK7XAAAAkElEQVR4nGMQBIHdG8EUA4iIY+DZCONIOzAwdMM44gwMDLdgHBkgh518juiMPSBO5GsQRyoDzFk2G4MjOnMTmOMN4ki/XQzkpBo0gk0rOK3AwFDdUAjmRLILv+ySZ9kI5ghf+C0oeuAy1NJahpkJbIZQjmgDA6823KeCBz7CvS1t3HDYGqurxWfOnOmM0AMFAFJYLUVYbsL4AAAAAElFTkSuQmCC";

        public static string Error { get => Storager.Me.error; }

        readonly TabItem[] tabItems = new TabItem[] {
            new TabItem("Для детей", "kids", img_a),
            new TabItem("Спорт", "wellness", img_b),
            new TabItem("Культура", "culture", img_c),
            new TabItem("Экскурсии", "excursion", img_d),
            new TabItem("Образ жизни", "lifestyle", img_e),
            new TabItem("Вечеринки", "parties", img_f),
            new TabItem("Образование", "study", img_g),
            new TabItem("Онлайн", "online", img_h),
            new TabItem("Шоу", "shows", img_i),
        };
        public TabItem[] TabItems { get => tabItems; }
    }
}
