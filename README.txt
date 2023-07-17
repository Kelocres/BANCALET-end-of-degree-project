Hola

Me llamo Miguel Ángel, y este es el proyecto Unity de Bancalet, mi proyecto gamedev que hago en solitario como trabajo de fin de grado.

El enlace para descargar el ejecutable es éste: https://drive.google.com/file/d/1UvzbC_wqCflPQAlq2ELf1ccBhJUNbYnZ/view?usp=sharing
CONTROLES:

    - EL RATÓN: Con él se interaccionan los botones de los menús y los elementos en el escenario. Respecto a los segundos, un elemento será interactuable cuando nos acerquemos a él y cambie a color rojo, y se podrá interaccionar con una acción contextual o una acción con item (algunos permiten ambas):

        - Click izquierdo: Activa la acción con item. Según el slot seleccionado en la barra de items, se permitirá (o no) una acción con el elemento del escenario.

        - Click derecho: Activa la acción contextual, la cual será independiente del slot seleccionado.

        - Rueda del ratón: Permite cambiar de slot en la barra de items.

    

    - MOVIMIENTO: El eje de movimiento del personaje jugador dependerá de la cámara.

        - TECLA W: Mover hacia delante.

        - TECLA A: Mover hacia la izquierda.

        - TECLA S: Mover hacia atrás.

        - TECLA D: Mover hacia la derecha.

    - ABRIR MENÚS: Para abrir un menú se deberá pulsar su tecla correspondiente. Para salir de él, se deberá pulsar el botón del menú que ponga TORNAR AL JOC:

        - TECLA ESC: Abrir el menú de pausa.

        - TECLA I: Abrir el menú de inventario.

    INTERACTUAR CON LAS PLANTAS:
        - SEMBRAR PLANTA: Para crear una planta, el jugador deberá seleccionar un item semilla en la barra de items y usarlo en una de las casillas de tierra (los  cuadrados marrones que hay en el suelo). Aparte de las semillas que hay en el inventario al iniciar partida, el jugador puede conseguir más en el inventario seleccionando el slot del item y pulsando el botón CREATE SEEDS.

        - REGAR PLANTA: Para que la planta crezca y dé cosecha, el jugador deberá regar cada día la tierra en la que se ha plantado. Para ello, debe utilizar el item herramienta Regadera (Arruixadora) en la casilla de tierra, la cual cambiará de textura.

        (en la información del item que se muestra en el inventario, cada semilla dice cuándo empieza a dar frutos en caso de regarse cada día)

        - COSECHAR: Si el jugador riega la planta cada día, avanzará de fase hasta llegar a las que den frutos. En estas fases, cada día se generará una cantidad de frutos aleatoria (esa cantidad puede ser 0). Cuando la planta muestre que tiene frutos, el jugador los puede recoger activando la acción contextual (click derecho) en la planta. El jugador deberá seguir regando cada día para que siga dando más cosecha.

        Si no se recogen los frutos, éstos no se acumulan durante los días, así que se deberá recoger siempre que la planta muestre que tenga cosecha.

        - CORTAR PLANTA: Tanto sea porque la planta haya muerto o el jugador quiera aprovechar la casilla de tierra para plantar otra cosa, el jugador puede eliminar la planta utilizando con ella el item herramienta Hacha (Destral).

    SISTEMA DE RESISTENCIA: La barra vertical de color amarillo indica la resistencia del personaje jugador, la cual se gasta utilizando items herramientas para progresar con el cuidado de las plantas. También se gasta cuando el valor de Alimentación esté tan bajo que se agotará de hambre cada hora. Cuando el valor de Resistencia llegue a 0, el jugador no podrá utilizar ninguna herramienta. Para regenerarla, el jugador  tendrá que alimentarse o irse a dormir para despertar al día siguiente con más energía; pero está dependerá del valor de Alimentación de cuando se fue a dormir.

    SISTEMA DE ALIMENTACIÓN: La barra vertical verde indica el valor de alimentación del personaje jugador. Ésta se consumirá progresivamente cada hora, y el jugador deberá evitar que se vacíe consumiendo items que sean cosecha o consumibles (Fogassa de Pa, Mel y Coca, están en el inventario).

        - Si el valor de Alimentación es alto, cuando se consuma en cada hora subirá ligeramente el valor de Resistencia.

        - Si el valor de Alimentación es muy bajo, el de Resistencia irá disminuyendo en cada hora.

        - Cuando el jugador vaya a dormir, según el valor que tenga cuando lo haga  repercutirá en los valores de Alimentación y Resistencia que tendrá a la mañana  siguiente (en el Menú Dormir hay un texto que te indica si tienes el valor de Alimentación lo suficientemente alto).

    DORMIR: El jugador abrirá el Menú Dormir activando la acción contextual (click derecho) en la puerta de la casa.

    INVENTARIO:
        - VER INFORMACIÓN DE UN ÍTEM: Selecciona un slot del inventario y aparecerá su información en la ventana.

        - MODIFICAR POSICIÓN DE SLOTS: Mantén presionado un item y arrástralo hacia otro slot, tanto del inventario como de la barra de items. Si lo mueves hacia un slot ocupado por otro item, estos se intercambian la posición.

        (Aún no está habilitado que se sumen las cantidades de dos slots del mismo item)

        - CREAR SEMILLAS: Selecciona un item cosecha en el inventario y pulsa el  botón CREATE SEEDS para crear una semilla de la planta de la cosecha seleccionada.

    BARRA DE ITEMS: Para cambiar de slot seleccionado puedes pulsar la tecla numérica correspondiente, seleccionarlo con el click izquierdo del ratón o cambiando con la rueda del ratón.