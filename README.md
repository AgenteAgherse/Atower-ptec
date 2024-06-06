# Prueba Técnica Atower
    * Nombre Aplicante: Agustín Hernández Serpa

# Requerimientos

Desarrollar un Microservicio web con los siguientes EndPoints:
1. Análisis

    1.1. Un endpoint que reciba una URL y me retorne un PDF de esa URL

    1.2. Un endpoint que reciba varias URL máximo 6, y me retorne un solo PDF de todas las URL anteriores

    1.3. Un endpoint que reciba varias URL máximo 6, y me retorne un ZIP con un solo PDF de todas las URL anteriores, y guardar en una tabla en SQL SERVER el base64 del zip para consultas posteriores

    1.4. Un endpoint que me permita descargar un ZIP ya generado anteriormente por un identificador único

    1.5. El microservicio debe estar corriendo en ISS

## Dependendencias Usadas
Las dependencias que se usaron dentro del proyecto son:
```C#
    Microsoft.EntityCore
    DinkToPdf
    System.IO.Compression
```

## Problemas Adicionales
Uno de los problemas que se pueden generar es que no aparece el dll libwkhtmltox.dll. Para descargar dicho archivo, dirigirse al siguiente link:

**https://github.com/rdvojmoc/DinkToPdf/tree/master/v0.12.4**

## Conexión a la Base de Datos
Dentro del documento AppSettings.json aparece la propiedad ```DefaultConnection```. Dicha propiedad cuenta con la información del servidor (en este caso local), usuario, base de datos, contraseña y aprobación a fuente confiable.

appsettings.json ➡️ DefaultConnection


    ``` Server=AGUSTÖN\\SQLEXPRESS;Database=prueba_tecnica;User Id=sa;Password=toor;Trust Server Certificate=true ```


## Estructura de la Base de Datos

Actualmente, la base de datos cuenta con una sola tabla llamada zips. Guarda el Id, el archivo (conjunto de bytes) y hora y fecha de creación.

```sql
    USE [prueba_tecnica]
    GO
    SET ANSI_NULLS ON
    GO
    SET QUOTED_IDENTIFIER ON
    GO
    CREATE TABLE [dbo].[zips](
        [id] [int] IDENTITY(1,1) NOT NULL,
        [archivo] [text] NOT NULL,
        [createdAt] [datetime] NULL,
    PRIMARY KEY CLUSTERED 
    (
        [id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    GO
    ALTER TABLE [dbo].[zips] ADD  DEFAULT (getdate()) FOR [createdAt]
    GO
```

## Estructura del Proyecto
El proyecto actualmente cuenta con 4 Carpetas. Estas son:

* **Controllers:**: Generan direccionamiento dentro de la API
* **Services:**: Realizan la lógica del programa (Pasar url a pdf y agregarlos en zips)
* **Models:** Entidades Encontradas en la base de datos
* **Data:** Capa intermedia entre el modelo y el servicio (Conexión y manipulación de la base de datos).

## Correr el Programa

El programa debido a que cuenta con la implementación de Swagger, se puede hacer uso de los controllers en la ventana:
    **https://localhost:7091/swagger/index.html**

Automáticamente, el programa debería poder redirigir a esta pestaña. Sin embargo, en caso que no, puede dirigirse a este link.