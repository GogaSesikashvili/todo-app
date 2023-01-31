using NUnit.Framework;
using Todo.Data.Domain;

namespace Todo.Tests
{
    /// <summary>
    /// Tests for domain model.
    /// </summary>
    [TestFixture]
    public class DomainModelTests
    {
        /// <summary>
        /// Tests transient entities.
        /// </summary>
        [Test]
        public void Different_transient_entities_should_not_be_equal()
        {
            var tl1 = new TodoList();
            var tl2 = new TodoList();

            var te1 = new TodoEntry();
            var te2 = new TodoEntry();

            Assert.AreNotEqual(tl1, tl2, "Different transient entities should not be equal.");
            Assert.AreNotEqual(te1, te2, "Different transient entities should not be equal.");
        }

        /// <summary>
        /// Checks if entities with different id are not equal.
        /// </summary>
        /// <param name="id1">First id.</param>
        /// <param name="id2">Second id.</param>
        [Test]
        [TestCase(9, 2)]
        [TestCase(55, 32)]
        [TestCase(2, 3)]
        [TestCase(766, 96)]
        public void Entities_with_different_id_should_not_be_equal(int id1, int id2)
        {
            var tl1 = new TodoList { Id = id1 };
            var tl2 = new TodoList { Id = id2 };

            var te1 = new TodoList { Id = id1 };
            var te2 = new TodoList { Id = id2 };

            Assert.AreNotEqual(tl1, tl2, "Entities with different ids should not be equal.");
            Assert.AreNotEqual(te1, te2, "Entities with different ids should not be equal.");
        }

        /// <summary>
        /// Checks if entity is not equal to transient entity.
        /// </summary>
        [Test]
        public void Entity_should_not_equal_transient_entity()
        {

            var tl1 = new TodoList { Id = 1 };
            var tl2 = new TodoList();

            var te1 = new TodoEntry { Id = 1 };
            var te2 = new TodoEntry();

            Assert.AreNotEqual(tl1, tl2, "Entity should not be equal to transient entity.");
            Assert.AreNotEqual(te1, te2, "Entity should not be equal to transient entity.");
        }

        /// <summary>
        /// Checks if references to the same transiant entity are equal.
        /// </summary>
        [Test]
        public void References_to_same_transient_entity_should_be_equal()
        {
            var tl1 = new TodoList();
            var tl2 = tl1;

            var te1 = new TodoEntry();
            var te2 = te1;

            Assert.AreEqual(tl1, tl2, "References to the same transient entity should be equal.");
            Assert.AreEqual(te1, te2, "References to the same transient entity should be equal.");
        }

        /// <summary>
        /// Checks if entities with same id but different type are not equal.
        /// </summary>
        [Test]
        public void Entities_with_same_id_but_different_type_should_not_be_equal()
        {
            var id = 7;

            var tl = new TodoList { Id = id };

            var te = new TodoEntry { Id = id };

            Assert.AreNotEqual(tl, te, "Entities with the same id and different types should not be equal.");
        }
    }
}