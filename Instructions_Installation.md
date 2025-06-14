# 📋 Instructions d'Installation - Projet CRUD Téléphones

## 🎯 Guide pas à pas pour Visual Studio

### Étape 1 : Préparation de l'environnement
1. **Ouvrir Visual Studio 2022**
2. **Créer un nouveau projet** :
   - Fichier → Nouveau → Projet
   - Sélectionner "Application WPF (.NET)"
   - Nom du projet : `TelephoneCRUD`
   - Choisir .NET 6.0 ou plus récent
   - Cliquer sur "Créer"

### Étape 2 : Configuration du projet
1. **Remplacer le fichier de projet** :
   - Supprimer le contenu de `TelephoneCRUD.csproj`
   - Copier le contenu fourni dans le fichier

2. **Restaurer les packages NuGet** :
   - Clic droit sur le projet → "Gérer les packages NuGet"
   - Ou utiliser la Console du Gestionnaire de package :
   ```
   Update-Package -Reinstall
   ```

### Étape 3 : Création de la structure de dossiers
Créer les dossiers suivants dans le projet :
- `Models`
- `Data`
- `Services`

### Étape 4 : Ajout des fichiers
1. **Supprimer les fichiers par défaut** :
   - Supprimer `MainWindow.xaml` et `MainWindow.xaml.cs` existants

2. **Ajouter tous les fichiers fournis** dans leurs dossiers respectifs :
   - `Models/Telephone.cs`
   - `Data/TelephoneContext.cs`
   - `Services/TelephoneService.cs`
   - `MainWindow.xaml` et `MainWindow.xaml.cs`
   - `ListeTelephoneWindow.xaml` et `ListeTelephoneWindow.xaml.cs`
   - `App.xaml` et `App.xaml.cs`

### Étape 5 : Compilation et test
1. **Compiler le projet** :
   - Génération → Générer la solution (Ctrl+Shift+B)

2. **Résoudre les erreurs éventuelles** :
   - Vérifier que tous les using sont corrects
   - Vérifier que tous les fichiers sont dans les bons dossiers

3. **Exécuter l'application** :
   - Appuyer sur F5 ou cliquer sur "Démarrer"

## 🔧 Configuration alternative avec .NET CLI

Si vous préférez utiliser la ligne de commande :

```bash
# 1. Créer le dossier du projet
mkdir TelephoneCRUD
cd TelephoneCRUD

# 2. Créer le projet WPF
dotnet new wpf

# 3. Ajouter les packages Entity Framework
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design

# 4. Copier tous les fichiers fournis

# 5. Restaurer les dépendances
dotnet restore

# 6. Compiler
dotnet build

# 7. Exécuter
dotnet run
```

## 🗃️ Configuration de la base de données

### Option 1 : LocalDB (Recommandée)
La configuration par défaut utilise LocalDB qui est automatiquement installé avec Visual Studio.

### Option 2 : SQL Server Express
Si vous préférez SQL Server Express, modifiez la chaîne de connexion dans `TelephoneContext.cs` :

```csharp
optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=TelephoneDB;Trusted_Connection=true;");
```

### Option 3 : SQL Server complet
Pour SQL Server complet :

```csharp
optionsBuilder.UseSqlServer(@"Server=localhost;Database=TelephoneDB;Trusted_Connection=true;");
```

## ✅ Vérification de l'installation

### Tests à effectuer après installation :

1. **L'application se lance** sans erreur
2. **La base de données se crée** automatiquement
3. **Ajouter un téléphone** fonctionne
4. **La liste se met à jour** correctement
5. **Les opérations CRUD** fonctionnent toutes

### Données de test suggérées :

| IMEI | Marque | Modèle | Prix |
|------|--------|--------|------|
| 123456789012345 | Apple | iPhone 14 | 999.99 |
| 987654321098765 | Samsung | Galaxy S23 | 849.99 |
| 456789123456789 | Google | Pixel 7 | 699.99 |

## 🚨 Résolution des problèmes courants

### Erreur : "LocalDB n'est pas disponible"
**Solution** : Installer SQL Server Express LocalDB depuis le site Microsoft

### Erreur : "Package non trouvé"
**Solution** : 
```bash
dotnet nuget locals all --clear
dotnet restore
```

### Erreur : "Impossible de créer la base de données"
**Solution** : Vérifier les permissions et la chaîne de connexion

### Interface qui ne s'affiche pas
**Solution** : Vérifier que tous les fichiers XAML sont correctement copiés

## 📞 Support

Si vous rencontrez des problèmes :
1. Vérifiez que tous les fichiers sont présents
2. Vérifiez la version de .NET (6.0 minimum)
3. Vérifiez que LocalDB est installé
4. Nettoyez et reconstruisez la solution

## 🎉 Félicitations !

Une fois l'installation terminée, vous devriez avoir une application WPF fonctionnelle avec :
- ✅ Interface de gestion CRUD
- ✅ Base de données automatiquement créée
- ✅ Validation des données
- ✅ Interface de liste détaillée
- ✅ Fonctionnalités d'export

L'application est maintenant prête à être utilisée et personnalisée selon vos besoins !