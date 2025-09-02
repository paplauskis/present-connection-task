import PackageRow from './PackageTableRow'

export default function PackageTable({ packages, onStatusChange }) {
  return (
    <table>
      <thead>
        <tr>
          <th>Tracking number</th>
          <th>Sender name</th>
          <th>Sender address</th>
          <th>Sender phone number</th>
          <th>Recipient name</th>
          <th>Recipient address</th>
          <th>Recipient phone number</th>
          <th>Status</th>
          <th>Created At</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tbody>
        {packages.map((pkg) => (
          <PackageRow key={pkg.id} pkg={pkg} onStatusChange={onStatusChange} />
        ))}
      </tbody>
    </table>
  )
}
