# PEC4-Titanium

Versión de subida: 1

In case you get the "Window layout error" on loading unity project, follow the instructions in this video: https://www.youtube.com/watch?v=hGukVu1DR18&ab_channel=GameTrick

**Explicación del gameplay**

Se trata de un juego de plataformas en 2D. Ambientado en un mundo cyberpunk con un estilo pixel art. En este juego encontraremos tres niveles diferentes. El primero será un nivel clásico de plataformas, deberemos recoger unos objetos y abrir una puerta, en el camino encontraremos enemigos y NPCs. El segundo lucharemos contra un boss con dos fases. Y para acabar será un nivel de persecución en vertical, deberemos huir y evitar salir por el límite inferior de la cámara.

Los controles son senzillos: las flechas para desplazarnos y saltar y la tecla D para atacar. Los ataques pueden ser usados para protegernos de los proyectiles y rebotarlos hacia los enemigos.

Cada nivel es una escena, además de la del menú principal.

**Proceso del desarrollo**

El desarrollo ha evolucionado respecto a los anteriores repositorios. Los mayores cambios han sido en el nuevo modo de ataque melé, las nuevas inteligencias artificiales y el modo de persecución en el nivel final.

_Ataque melé_

El personage dispone de un ataque melé con la espada. Puede atacar en el aire o en el suelo (animaciones diferentes para los dos casos, pero misma función). Estos ataques tienen un área de ataque que se activará cuando el jugador presione la D. Si en esta área se detecta que entra un enemigo, bala o boss, entonces invocará sus respectivos métodos. Detectar un marine, activará el método que daña al marine, igual que el boss. En cambio, para las balas, el área es mayor para facilitar la mecánica, este destruirá la bala enemiga e instanciará una nueva que irá en dirección opuesta y eliminará un enemigo si le impacta. Las balas rebotadas no afectan al boss.

_Inteligencias Artificiales_

Los enemigos, nombrados Marines, tienen un sistema de detección por distancia. Una vez el jugador entra en el rango de detección, el marine le disparará, y en caso de que el jugador trate de huir, el marine le perseguirá hasta que muera, mate al jugador o el jugador se distancie verticalmente.

La creación de un boss es algo nuevo en mis repositorios. Se ha desarrollado mayormente con el sistema de máquina de estados y códigos de comportamientos. Por lo que el funcionamiento de este funciona mayormente por eventos de animación y scripts behaviour que están incorporados en las animaciones. Sin embargo, los ataques siguen el mismo desarrollo que en los otros personages.

_Nivel de persecución_

A diferencia de los anteriores, la cámara no sigue al jugador. En este, cuando se activa la secuencia de huida, la cámara empezará a subir, simulando que el espacio se está inundando i que el jugador puede quedar atrás o morirá. Para ello se ha creado un punto Transform que sigue al jugador horizontalmente, pero verticalmente se le ha añadido una velocidad constante. Por lo que la cámara seguirá este punto en lugar de el personage.

Se ha hecho uso de la música para profundizar el ambiente, hay vibraciones que siguen el ritmo de la música. De esta manera tambien simula que el espacio se está como destruyendo.

_Sonidos y banda sonora_

Al ser un juego ambientado en un mundo Cyberpunk, se le ha dado importancia a los sonidos y banda sonora. Se ha escogido un repartorio de música sin copyright del género Synthwave y Dark Techno, y para los efectos una recopilación de sonidos sci-fi.

_Detección de bugs_

Boss: Como se ha dicho antes, el boss está desarrollado mayormente por animaciones. Esto afecta en un ataque en concreto: leap attak. Este ataque se trata de un salto, lanzar el martillo y caer en la ubicación de este rápidamente. Toda la secuencia es una animación, por lo que el BoxCollider se ha desacitvado para no crear una colisión estática mientras se produce el ataque. La ubicación final de esta animación está desplazada 4.5 a los lados. Para contrarrestar esta diferencia de animación, se ha incorporado un evento que modifica la posición inmediatamente cuando se acaba la animación. Sin embargo parece que queda un fotograma perdido con la posición antigua.

Además, esta animación permite al boss salirse de la sala de combate, pero también vuelve a entrar cuando la realiza en dirección a la sala. Este bug se puede solucionar haciendo que el boss ignore las colisiones con las paredes. O si queremos aprovehcar este bug para darle sensanción de poder, activar una animación que rompa la pared. A causa de falta de tiempo, no se ha podido desarrollar estas soluciones. Lamento los inconvenientes.

_Nivel de persecución_

Tras realizar el completo diseño del nivel, se descubrió que había plataformas en las que no llegaba del todo, haciendo que sólo hubiera 1 posible camino. Para evitar eso, se han eliminado los colliders verticales de las plataformas, de esta manera puede realizar un salto extra cuando toque el límite de la plataforma y salvar el error. Pero las consecuencias es que puede quedarse enganchado un muy breve tiempo.

**Comentarios**

Se puede observar que las sprites del boss están incompletas, se quería obtener unos colores diferentes al jugador, con tonos rojizos y anaranjados. Debido a falta de tiempo, no se ha podido acabar de pintar como se deseaba.

**Video demostración**

https://www.youtube.com/watch?v=05InS-t0gOU&ab_channel=JohhaannDev
