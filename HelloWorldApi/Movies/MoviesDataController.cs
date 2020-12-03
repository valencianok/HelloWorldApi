using HelloWorldApi.Global;
using HelloWorldApi.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldApi.Movies
{
    public class MoviesDataController : IDisposable
    {
        DocumentClient client;

        public MoviesDataController()
        {
            client = new DocumentClient(SolutionConfiguration.Instance.DBEndpointURI, SolutionConfiguration.Instance.DBAuthKey);
        }

        private Uri getMoviesCollectionORDocUri(Guid? docId)
        {
            if (docId.HasValue)
            {
                Uri docUri = UriFactory.CreateDocumentUri(SolutionConfiguration.Instance.DBName, "Items", docId.ToString());
                return docUri;
            }
            else
            {
                Uri collectionUri = UriFactory.CreateDocumentCollectionUri(SolutionConfiguration.Instance.DBName, "Items");
                return collectionUri;
            }
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            Uri collectionUri = getMoviesCollectionORDocUri(null);

            // Do the query
            var movies = new List<Movie>();
            using (var q = client.CreateDocumentQuery<Movie>(collectionUri).AsDocumentQuery())
            {
                // TODO: should allow filter, should have pagination

                // Iterate the results
                while (q.HasMoreResults)
                {
                    foreach (var movie in await q.ExecuteNextAsync<Movie>())
                    {
                        movies.Add(movie);
                    }
                }
            }

            return movies;
        }

        public async Task<Movie> GetMovieAsync(Guid id)
        {
            return await client.ReadDocumentAsync<Movie>(getMoviesCollectionORDocUri(id));
        }

        public async Task<Guid?> CreateMovieAsync(Movie movie)
        {
            movie.id = Guid.NewGuid();
            var r = await client.CreateDocumentAsync(getMoviesCollectionORDocUri(null), movie);
            if (r.StatusCode == System.Net.HttpStatusCode.Created)
                return movie.id;
            else
                return null;
        }

        public async Task<bool> UpdateMovieAsync(Guid id, Movie movie)
        {
            movie.id = id;
            var r = await client.ReplaceDocumentAsync(getMoviesCollectionORDocUri(id), movie);
            return r.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> DeleteMovieAsync(Guid id)
        {
            var r =  await client.DeleteDocumentAsync(getMoviesCollectionORDocUri(id));
            return r.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
