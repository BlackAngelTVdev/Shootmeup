/*
Il y a trois modes de fonctionnement possible de gitjournal:
1. Utiliser un seul repo, toujours le même. 
   Pour cela, il suffit de configurer la constante `defaultRepoUrl`.
   Si on ne spécifie pas d'utilisateurs Git, seuls les commits du propriétaire du repo sont pris
2. Avoir plusieurs repos que l'on veut suivre. 
   Pour cela, il faut configurer le tableau `repos`.
   Le nom qui apparaîtra dans la liste déroulante est le nom du repo, sauf si un alias est précisé. 
   Il est également possible de spécifier la liste des contributeurs (utilisateurs Git) que l'on veut voir apparaître dans le journal.
3. Si aucune des deux options précédente n'a été configurée: on a un simple champ texte de saisie de l'URL du repos pour lequel on veut faire le journal. 
   Dans ce cas, les commits pris en considération sont également ceux du propriétaire du Reto.
*/

const defaultRepo = { url: "https://github.com/djdams1/P_oo-Shoot-me-up" };

const defaultAuthToken = "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"; // Make sure the token has full private repo privileges
