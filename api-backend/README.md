# Documentation du Back-End

## Prérequis d'installation

Pour faire fonctionner ce back-end, vous devez installer les éléments suivants:

1. **PostgreSQL 17**
2. **Java JDK** (compatible avec Spring 6.4.4, Java 21 dans notre cas )
3. **Maven**

## Configuration de la base de données

1. Installez PostgreSQL 17 depuis le [site officiel](https://www.postgresql.org/download/)
2. Créez une nouvelle base de données
3. Exécutez les scripts SQL dans l'ordre suivant:
    - `/sql/init.sql` - Crée la structure de la base de données
    - `/sql/fakedata.sql` - Remplit la base avec des données de test

## Démarrage du back-end

1. Clonez le dépôt du projet
2. Modifier le `application.properties` avec les données de votre serveur postgreSQL
3. Naviguez vers le répertoire du projet via le terminal
4. Exécutez la commande: `mvn spring-boot:run`
5. Le serveur devrait démarrer sur le port par défaut (généralement 8080)

## Vérification du fonctionnement

Pour vérifier que votre back-end fonctionne correctement:
- Accédez à `http://localhost:8080/`
- Vous pourrez utiliser les différentes routes de cette API