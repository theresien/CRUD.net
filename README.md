# 📱 Projet CRUD Téléphones - C# WPF

## Description
Application de gestion des téléphones développée en C# avec WPF et Entity Framework. Cette application permet d'effectuer toutes les opérations CRUD (Create, Read, Update, Delete) sur une base de données de téléphones.

## Fonctionnalités

### ✨ Fonctionnalités principales
- **Ajouter** un nouveau téléphone avec validation des données
- **Modifier** les informations d'un téléphone existant
- **Supprimer** un téléphone avec confirmation
- **Consulter** la liste complète des téléphones
- **Rechercher** des téléphones par marque, modèle ou IMEI
- **Exporter** la liste vers un fichier CSV/TXT

### 📊 Modèle de données
- **IMEI** : Clé primaire (15 caractères max)
- **Marque** : Marque du téléphone (50 caractères max)
- **Modèle** : Modèle du téléphone (50 caractères max)
- **Prix** : Prix en euros (décimal avec 2 décimales)
- **Date d'ajout** : Date et heure d'ajout automatique

## 🏗️ Architecture

### Structure du projet
```
TelephoneCRUD/
├── Models/
│   └── Telephone.cs          # Modèle de données
├── Data/
│   └── TelephoneContext.cs   # Contexte Entity Framework
├── Services/
│   └── TelephoneService.cs   # Logique métier et accès aux données
├── MainWindow.xaml           # Interface principale
├── MainWindow.xaml.cs        # Code-behind principal
├── ListeTelephoneWindow.xaml # Interface de liste détaillée
├── ListeTelephoneWindow.xaml.cs
├── App.xaml                  # Configuration application
├── App.xaml.cs
└── TelephoneCRUD.csproj     # Configuration projet
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
   mkdir TelephoneCRUD
   cd TelephoneCRUD
   
   # Copier tous les fichiers fournis dans ce dossier
   ```

2. **Restaurer les packages NuGet**
   ```bash
   dotnet restore
   ```

3. **Créer la base de données**
   ```bash
   # La base de données sera créée automatiquement au premier lancement
   # grâce à EnsureCreated() dans TelephoneService
   ```

4. **Compiler et exécuter**
   ```bash
   dotnet build
   dotnet run
   ```

### Configuration de la base de données

La chaîne de connexion par défaut utilise LocalDB :
```csharp
Server=(localdb)\mssqllocaldb;Database=TelephoneDB;Trusted_Connection=true;
```

Pour modifier la base de données, éditez le fichier `Data/TelephoneContext.cs`.

## 📖 Guide d'utilisation

### Interface principale (MainWindow)
1. **Ajouter un téléphone** :
   - Remplir tous les champs obligatoires
   - Cliquer sur "➕ Ajouter"

2. **Modifier un téléphone** :
   - Sélectionner un téléphone dans la liste
   - Modifier les champs souhaités
   - Cliquer sur "✏️ Modifier"

3. **Supprimer un téléphone** :
   - Sélectionner un téléphone ou saisir l'IMEI
   - Cliquer sur "🗑️ Supprimer"
   - Confirmer la suppression

### Interface de liste détaillée (ListeTelephoneWindow)
- **Recherche** : Utiliser la barre de recherche pour filtrer
- **Export** : Exporter la liste vers CSV/TXT
- **Statistiques** : Voir le nombre total, prix moyen et valeur totale

## 🔧 Fonctionnalités techniques

### Validation des données
- Tous les champs sont obligatoires
- L'IMEI doit être unique
- Le prix doit être un nombre positif
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
- Ajout de photos pour les téléphones
- Système de catégories
- Historique des modifications
- Sauvegarde/restauration
- Interface multi-langues
- Rapports avancés

## 📄 Licence
Ce projet est fourni à des fins éducatives et peut être librement modifié et distribué.