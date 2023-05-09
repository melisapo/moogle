# Proyecto de Programación I. Facultad de Matemática y Computación. Universidad de La Habana. Curso 2023. #
# Moogle!
 Melissa Gutierrez Hernandez
 CC-122

# Introduccion

Con el objetivo de superarme intelectualmente y mejorar mis habilidades de programacion he aceptado el desafio presentado por el Claustro de  Maestros de Programacion para  intentar desarrollar el codigo interno(la parte logica) del Proyecto Moogle!.

# Descripción

Mi Propuesta de este proyecto comienza con: 

 PlainLoader: Clase dedica a la adquisicion y procesamiento de los documentos que se usaran como base datos.Ademas de un contructor que es llamado directamente por el moogle.server y sera consultado cada vez que se introduce una nueva query.
 
 MetaData: Clase que define todas las propiedades de las palabras almacenadas.

 Score: En esta clase se realizan toda la logica fundamental para hacer funcionar la busqueda.

 Snippet: Esta clase se ocupa de buscar en el documento correspondiente una breve parte del contenido buscado en Query.

 Suggestion: Esta clase en caso de que la query sea introduccida con errores los rectifica y se lo dice al usuario , ademas de para mejor eficiencia lo rectifica automaticamente al momento de realizar la busqueda. 

Operators: Esta clase se ocupa de implementar la logica de todos los operadores propuesto, ademas de ejecutarlo en el momento necesario. 

Base: Esta es una clase de apoyo aqui se realiza diversar funciones complementarias.

QueryDocuments: Aqui se procesa el query y se guarda las palabras de la misma.

items: Esta clase es la encargada de formar el objeto a entregar en la busqueda.

SearchItem: Esta clase es el tipo de obejto que se entregara en la busqueda.

SearchResult:Esta clase agrupa SearchItem para entregar un conjunto de objetos en la busqueda

Moogle:Esta es la ultima clase y la original, recibe la query y los documentos cargados previmente. Ademas forma la estructura central de mi proyecto al encargarse de llamar y cablear todas las clases antes propuestas.Y por ultimo es la que entrega a la busqueda el objeto a mostrar y la sugerencia.

# Detalles Generales

El Tipo Principal de Objeto utilizado es el Diccionario y algunas listas, juntos a otras clases mas genericas como soporte(array,string,etc)

Entre los princpales diccionarios creados esta:

Documents:Un diccionario donde a cada documeto le corresponde un diccionaro con sus palabras y este a su vez a cada palabra le corresponde un objeto metadata donde guardar todas los detalles de la misma().

Twords:Un diccionario que guarda todas las palabras que hay entre todos los documentos con la cantidad de documentos en la que aparece.

Score:Diccionario donde a cada documento(key) le corresponde su Score

Query:Diccionario que contiene las palabras de la query recibida por el usuario con sus detalles.

Operators: Diccionario que almacena si aparecen los operadores de la query y sus detalles

Snippet: Diccionario donde cada documento guarda una porcion del mismo relacionado con la query.

Otros objetos importantes son:

Item: Es el objeto que se devuelve por el metodo moogle.

Suggest: Corrige al usuario si la query fue escrita erroneamente.

SRanking(ScoreRanking): Una lista donde se organiza el score de mayor a menor.

# Detalle Sobre el proceso de Busqueda

El proceso de busqueda incia con la lectura y procesamiento de todos los documentos por parte de "PlainLoader" donde se guarda cada documento con sus palabras respectivas y los detalles de la mismas en Diccionarios, con el objetivo de facilicitar la busqueda posterior se a empleado un metodo que filtra automaticamente toda aquella palabra que aparece en mas del 80% de los documentos. Despues se procesa la query al ser introducida donde se extrae las  palabras que contiene guardandola en un diccionario y se revisa si contiene algun operador predefinido anteriormente dentro de un diccionario de char.
Despues se revisa si la query tiene alguna palbra erronea y la arregla.Consecutivamente se busca el Score donde a travez del Modelo vectorial de mineria de datos(concretamenta mediante la similitud cosénica entre los vectores formados por el documento y la búsqueda usando el calculo TF-IDF) calculamos la similitud que tiene cada documento con la query dandole valores entre 0 y 1 para asi conformar el score. Con el objetivo de conocer los mejores score en orden descendiente de mayor a menos se aplica el metodo SRanking formando asi una lista donde se encuentra los mejores Scores organizado, ademas para mantener normalizada la cantidad de scores presentada se saca un promedio de score obetinido y se toma como cota inferior para calificar en la lista.Una vez terminado esto se busca dentro del documento una porcion del misma para presentar como snippet ,esto se emplea al acceder a las posiciones de la palabras y escojiendo una vencindad de ella para presentar.Por ultimo se crea el objeto searchItem con cada resultado encontrado y se une en un searcResult para ser enviado al server. Se debe tener en cuenta que si en la query se introduce un operador entonces automaticamente se ejecuta la clase Operators donde en dependencia del operador recibido aumenta o disminuye porcentualmente el score y relevancia del documento.

# Defectos del Proyecto 

Como todo ser humano yo no soy perfecto y por transitividad mi proyecto tampoco por los tanto como motivo de autocritica expongo algunos defectos de mi proyecto:

- El metodo le lectura de PlainLoader solo acepta txt y al ser leido por streamreader solo puede reconocer signos de puntuacion y demas en formatos UTF-8.
- No se implemento ningun metodo para reconocer lecturas de numeros en la query o los documentos.
- Si se emplea algun operador en alguna mal escrita en la query este no funcionara
- El operador ~ solo escoje la palabra anterior a ella si esta continua o separada por solo un espacio.
- En el Score hay valores(son 6) que dan error por division por 0.

Algunos de estos errores se esperan arreglar.Ademas de seguir testeando para encontrar y rectificar en medida de los posible otros errorres.

# Desarrollo futuro

Para seguir mejorando y optimizando este proyecto espero poder implementar un sistema de sinonimos y antonimos ademas de mejor y optimizar el rendimiento general del mismo.








