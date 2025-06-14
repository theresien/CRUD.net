# 👥 Projet CRUD Utilisateurs - C# WPF

## Description
Application de gestion des utilisateurs développée en C# avec WPF et Entity Framework. Cette application permet d'effectuer toutes les opérations CRUD (Create, Read, Update, Delete) sur une base de données d'utilisateurs.

## Fonctionnalités

### ✨ Fonctionnalités principales
- **Ajouter** un nouvel utilisateur avec validation des données
- **Modifier** les informations d'un utilisateur existant
- **Supprimer** un utilisateur avec confirmation
- **Consulter** la liste complète des utilisateurs
- **Rechercher** des utilisateurs par matricule, nom, prénom ou email
- **Exporter** la liste vers un fichier CSV/TXT

### 📊 Modèle de données
- **Matricule** : Clé primaire (20 caractères max)
- **Nom** : Nom de famille (50 caractères max)
- **Prénom** : Prénom (50 caractères max)
- **Email** : Adresse email unique (100 caractères max)
- **Date de création** : Date et heure d'ajout automatique

## 🏗️ Architecture

### Structure du projet
```
UserCRUD/
├── Models/
│   └── User.cs                # Modèle de données
├── Data/
│   └── UserContext.cs         # Contexte Entity Framework
├── Services/
│   └── UserService.cs         # Logique métier et accès aux données
├── MainWindow.xaml            # Interface principale
├── MainWindow.xaml.cs         # Code-behind principal
├── ListeUserWindow.xaml       # Interface de liste détaillée
├── ListeUserWindow.xaml.cs
├── App.xaml                   # Configuration application
├── App.xaml.cs
└── UserCRUD.csproj           # Configuration projet
```

### Technologies utilisées
- **.NET 6** avec WPF
- **Entity Framework Core 7.0** pour l'accès aux données
- **SQL Server LocalDB** pour la base de données
- **XAML** pour l'interface utilisateur

## 🚀 Installation et Configuration

### Prérequis
- Visual Studio 2022 ou plus récent
- .NET 6.0 SDK
- SQL Server LocalDB (inclus avec Visual Studio)

### Étapes d'installation

1. **Créer le projet**
   ```bash
   # Créer un nouveau dossier pour le projet
   mkdir UserCRUD
   cd UserCRUD
   
   # Copier tous les fichiers fournis dans ce dossier
   ```

2. **Restaurer les packages NuGet**
   ```bash
   dotnet restore
   ```

3. **Créer la base de données**
   ```bash
   # La base de données sera créée automatiquement au premier lancement
   # grâce à EnsureCreated() dans UserService
   ```

4. **Compiler et exécuter**
   ```bash
   dotnet build
   dotnet run
   ```

### Configuration de la base de données

La chaîne de connexion par défaut utilise LocalDB :
```csharp
Server=(localdb)\mssqllocaldb;Database=UserDB;Trusted_Connection=true;
```

Pour modifier la base de données, éditez le fichier `Data/UserContext.cs`.

## 📖 Guide d'utilisation

### Interface principale (MainWindow)
1. **Ajouter un utilisateur** :
   - Remplir tous les champs obligatoires
   - Cliquer sur "➕ Ajouter"

2. **Modifier un utilisateur** :
   - Sélectionner un utilisateur dans la liste
   - Modifier les champs souhaités
   - Cliquer sur "✏️ Modifier"

3. **Supprimer un utilisateur** :
   - Sélectionner un utilisateur ou saisir le matricule
   - Cliquer sur "🗑️ Supprimer"
   - Confirmer la suppression

### Interface de liste détaillée (ListeUserWindow)
- **Recherche** : Utiliser la barre de recherche pour filtrer
- **Export** : Exporter la liste vers CSV/TXT
- **Statistiques** : Voir le nombre total, domaines email et dernier ajout

## 🔧 Fonctionnalités techniques

### Validation des données
- Tous les champs sont obligatoires
- Le matricule doit être unique
- L'email doit être unique et avoir un format valide
- Longueurs maximales respectées

### Gestion des erreurs
- Messages d'erreur explicites
- Validation côté client et serveur
- Gestion des exceptions de base de données

### Interface utilisateur
- Design moderne avec couleurs cohérentes
- Feedback visuel (hover, focus)
- Barre de statut informative
- Responsive design

## 🎨 Personnalisation

### Couleurs du thème
- **Primaire** : #3498DB (Bleu)
- **Succès** : #27AE60 (Vert)
- **Attention** : #F39C12 (Orange)
- **Erreur** : #E74C3C (Rouge)
- **Neutre** : #95A5A6 (Gris)

### Modification de l'apparence
Les styles sont définis dans `App.xaml` et peuvent être personnalisés selon vos besoins.

## 🔍 Dépannage

### Problèmes courants

1. **Erreur de connexion à la base de données**
   - Vérifier que LocalDB est installé
   - Vérifier la chaîne de connexion

2. **Erreur de compilation**
   - Vérifier que tous les packages NuGet sont installés
   - Nettoyer et reconstruire la solution

3. **Interface qui ne s'affiche pas correctement**
   - Vérifier la résolution d'écran
   - Redimensionner la fenêtre

## 📝 Notes de développement

### Bonnes pratiques implémentées
- **Séparation des responsabilités** (Models, Services, Views)
- **Programmation asynchrone** pour les opérations de base de données
- **Gestion des ressources** avec using et Dispose
- **Validation robuste** des données utilisateur
- **Interface utilisateur intuitive** et accessible

### Extensions possibles
- Ajout de photos de profil
- Système de rôles et permissions
- Historique des modifications
- Sauvegarde/restauration
- Interface multi-langues
- Rapports avancés

## 📄 Licence
Ce projet est fourni à des fins éducatives et peut être librement modifié et distribué.