export default function PersonInfo({ person, isSender }) {
  return (
    <section>
      <h2>{isSender ? 'Sender' : 'Recipient'}</h2>
      <p><strong>Name:</strong> {person.name}</p>
      <p><strong>Address:</strong> {person.address}</p>
      <p><strong>Phone:</strong> {person.phone}</p>
    </section>
  )
}