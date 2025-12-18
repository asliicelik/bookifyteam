## Bookify

### Mood-based Recommendations

This project now includes a **Mood Selector** feature that recommends music and books based on a mix of up to three moods.

- **Input**: User selects 1–3 moods (e.g., Calm, Intense, Melancholic).
- **Output order**:
  - Up to **5 songs** are shown first.
  - Then **1 book** that best matches the same mood mix.

#### Scoring Logic

Each song and book in the in-memory catalog has **mood affinity scores** in the range \([0, 1]\) for each mood (e.g., a song can be 0.9 Calm, 0.4 Nostalgic).

When the user selects moods, we assign weights:

- 1 mood → each selected mood weight = 1.0
- 2 moods → each selected mood weight = 0.5
- 3 moods → each selected mood weight ≈ 0.3333

For any item (song or book) we compute a total score:

\[
\text{score(item)} = \sum\_{\text{selected mood } i} \text{weight}(i) \times \text{affinity(item, mood}\_i)
\]

Songs are sorted by **score (descending)** and we take the top 5.  
Books are sorted by **score (descending)** and we take the top 1.

If there are no direct matches for the selected moods, the system falls back to the closest items (highest single affinity for any of the selected moods) and shows a friendly message.

You can access the feature at `Recommendation/Index` or via the **“Mood Selector”** link in the main navigation.
