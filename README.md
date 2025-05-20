# Parcial-Juego-TUVD

Juego proyecto para segundo parcial de las materias GameDesign y Motores de Desarrollo de la TUVD de la UTN

## Modo de desarrollo (Solo para colaboradores)

### Convenciones de codigo:

- Variables privadas (serializables o no) : **_camelCase**. Ejemplo: _playerSpeed
- Variables publicas: **camelCase**. Ejemplo: playerHealth
- Metodos publicos o privados: **PascalCase**. Ejemplo: GrabObject
- Propiedades: **PascalCase**. Ejemplo: Health

---

### Como trabajar en Unity

Cada implementacion se desarrolla en una Escena separada de la principal. Los assets van en sus respectivas carpetas (se agrega en caso de que falte alguna)

### Ramas de desarrollo

La rama **Main** va a ser la de produccion en la que esten las implementaciones confirmadas. **NO TRABAJAR EN ESTA RAMA.**  

Se trabaja en la rama **Develop**. Esta es la que cada uno va a tener que **mantener actualizada** en sus equipos. Cuando alguien quiera hacer un push de alguna implementacion, va a crear una rama temporal, hacer el push a la misma y luego combinarla con la rama **Develop**.  
Las rams temporales tienen que estar escrita de la siguiente manera:
{NombreIniciales} - Implementacion
>LI - PlayerMovement
